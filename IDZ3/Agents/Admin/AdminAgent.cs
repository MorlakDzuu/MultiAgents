using IDZ3.Agents.Base;
using IDZ3.Agents.Cooker;
using IDZ3.Agents.Dish;
using IDZ3.Agents.Equipment;
using IDZ3.Agents.Menu;
using IDZ3.Agents.Operation;
using IDZ3.Agents.Order;
using IDZ3.Agents.Process;
using IDZ3.Agents.Store;
using IDZ3.Agents.Visitor;
using IDZ3.DFs.DFCookers;
using IDZ3.DFs.DFDishCards;
using IDZ3.DFs.DFEquipment;
using IDZ3.DFs.DFMenu;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Order;
using IDZ3.MessageContracts.Process;
using IDZ3.MessageContracts.Store;
using IDZ3.MessageContracts.Visitor;
using IDZ3.MessagesContracts;
using IDZ3.Services.AgentFabric;
using System.Text.Json;

namespace IDZ3.Agents.Admin
{
    /// <summary>
    /// Управляющий агент
    /// </summary>
    public class AdminAgent : BaseAgent
    {
        // Агент склада, принадлежащий управляющему агенту
        private StoreAgent _storeAgent;
        private MenuAgent _menuAgent;
        private List<MenuDish> menuDishes;
        private List<DishCard> dishCards;
        private List<CookerRes> _cookers;
        private List<Oper> _equips;

        private List<CookerAgent> _cookerAgents;
        private List<EquipmentAgent> _equipmentAgents;

        public AdminAgent() : base( AgentRoles.ADMIN.ToString(), "head" )
        {
            _menuAgent = AgentFabric.MenuAgentCreate( Id );

            List<DFs.DFProducts.Product> products = DFs.DFProducts.DFProducts.GetValue().ProductsList;
            // Создаем агент склада
            _storeAgent = AgentFabric.StoreAgentCreate( Id, _menuAgent, products );

            dishCards = DFs.DFDishCards.DFDishCards.GetValue().DishCards;
            menuDishes = DFs.DFMenu.DFMenu.GetValue().MenuDiches;
            _cookers = DFs.DFCookers.DFCookers.GetValue().Cookers;
            _equips = DFs.DFEquipment.DFEquipment.GetValue().Equipment;

            _cookerAgents = new List<CookerAgent>();
            _cookers.ForEach( c => _cookerAgents.Add( AgentFabric.CookerAgentCreate( c, Id ) ) );

            _equipmentAgents = new List<EquipmentAgent>();
            _equips.ForEach( e => _equipmentAgents.Add( AgentFabric.EquipmentAgentCreate( e, Id ) ) );
        }

        /// <summary>
        /// Поведение управляющего агента
        /// </summary>
        new public void Action()
        {
            Lock();
            Message<AdminMessage> newMessage = GetMessage<AdminMessage>();
            switch ( newMessage.MessageContent.AgentType )
            {
                case ( AgentTypesAdminUnderstand.VISITOR ):
                    IAgent agent = _dFService.GetAgentById( newMessage.AgentFromId );
                    ProcessVisitorRequest( newMessage.MessageContent.SerializedData, newMessage.AgentFromId );
                    break;
                case ( AgentTypesAdminUnderstand.STORE ):
                    ProcessStoreRequest( newMessage.MessageContent.SerializedData );
                    break;
                case ( AgentTypesAdminUnderstand.ORDER ):
                    ProcessOrderRequest( newMessage.MessageContent.SerializedData, newMessage.AgentFromId );
                    break;
                case ( AgentTypesAdminUnderstand.OPERATION ):
                    ProcessOperationRequest( newMessage.MessageContent.SerializedData, newMessage.AgentFromId );
                    break;
                default:
                    break;
            }
            Unlock();
        }

        private CookerAgent GetCoookerForOperation()
        {
            List<CookerAgent> activeCookers = _cookerAgents.Where( ca => ca.Active ).ToList();
            int minOperCount = _cookerAgents[ 0 ].GetOperationCount();
            CookerAgent cookerForOper = _cookerAgents[ 0 ];

            foreach ( CookerAgent cooker in activeCookers )
            {
                if ( minOperCount == 0 )
                {
                    return cookerForOper;
                }

                if ( cooker.GetOperationCount() < minOperCount )
                {
                    minOperCount = cooker.GetOperationCount();
                    cookerForOper = cooker;
                }
            }

            return cookerForOper;
        }

        private EquipmentAgent GetEquipmentForOperation( int type )
        {
            List<EquipmentAgent> activeEquipmentByType = _equipmentAgents.Where( ea => ea.Active && ea.EquipType == type ).ToList();
            int minCookersCount = activeEquipmentByType[ 0 ].GetOperationsCount();
            EquipmentAgent equipForCooker = activeEquipmentByType[ 0 ];

            foreach ( EquipmentAgent equipmentAgent in activeEquipmentByType )
            {
                if ( minCookersCount == 0 )
                {
                    return equipForCooker;
                }

                if ( equipmentAgent.GetOperationsCount() < minCookersCount )
                {
                    minCookersCount = equipmentAgent.GetOperationsCount();
                    equipForCooker = equipmentAgent;
                }
            }

            return equipForCooker;
        }

        private void ProcessOperationRequest( string messageJson, string operationId )
        {
            OperationAgent operationAgent = ( OperationAgent ) _dFService.GetAgentById( operationId );
            CookerAgent cookerAgent = GetCoookerForOperation();
            EquipmentAgent equipmentAgent = GetEquipmentForOperation( operationAgent.GetEquipmentType() );

            equipmentAgent.AddOperation( operationAgent );
            operationAgent.SetEquipmentAgent( equipmentAgent );
            cookerAgent.PerfomOperation( operationAgent );

            SendMessageToAgent<ProcessRecieveMessage>( ProcessRecieveMessage.ProcessOperationReservedMessage( 
                new ProcessOperationReserved( operationId, cookerAgent.Id, equipmentAgent.Id ) ), operationAgent.GetProcessId() );
        }

        private void ProcessOrderRequest( string messageJson, string orderId )
        {
            OrderAdminMessage message = JsonSerializer.Deserialize<OrderAdminMessage>( messageJson );

            switch ( message.AdminMessageType )
            {
                case OrderAdminMessageType.RESERVE_PRODUCT:
                    OrderReserveProductMessage reserveMeassge = JsonSerializer.Deserialize<OrderReserveProductMessage>( message.SerializedData );
                    StoreRecieveMessage messageContent = new StoreRecieveMessage(
                        StoreActionTypes.CHECK_PRODUCT,
                        reserveMeassge.ProductType,
                        reserveMeassge.Amount,
                        orderId,
                        reserveMeassge.DishAgentId );
                    SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );

                    break;
                case OrderAdminMessageType.DISH_IS_READY:
                    OrderDishReadyMessage dishReadyMessage = JsonSerializer.Deserialize<OrderDishReadyMessage>( message.SerializedData );
                    StoreRecieveMessage messageContentDishReady = new StoreRecieveMessage( StoreActionTypes.DISH_READY, 0, 0, orderId, dishReadyMessage.DishAgentId );
                    SendMessageToAgent<StoreRecieveMessage>(messageContentDishReady, _storeAgent.Id );

                    break;
                default:
                    break;
            }
        }

        private void ProcessStoreRequest( string messageJson )
        {
            StoreCheckResultMessage message = JsonSerializer.Deserialize<StoreCheckResultMessage>( messageJson );

            if ( message.Result )
            {
                StoreRecieveMessage messageContent = new StoreRecieveMessage( 
                    StoreActionTypes.RESERVE_PRODUCT,
                    message.ProductType,
                    message.Amount, 
                    message.OrderId,
                    message.DishId );

                SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );
            } else
            {
                // TODO
            }
        }

        private void ProcessVisitorRequest( string messageJson, string visitorId )
        {
            VisitorAdminMessage message = JsonSerializer.Deserialize<VisitorAdminMessage>( messageJson );

            switch ( message.ActionType )
            {
                case ( VisitorAdminActionTypes.MENU_REQUEST ):
                    menuDishes = _menuAgent.GetActualMenu();
                    List<VisitorMenuDish> visitorMenu = menuDishes.FindAll( d => d.Card.Time < message.TimeLimit )
                        .ConvertAll<VisitorMenuDish>( d => new VisitorMenuDish( d.Id, d.Card.Name, d.Price, d.Card.Time, d.Active ) );

                    SendMessageToAgent<VisitorRecieveMessage>( VisitorRecieveMessage.CreateVisitorGetMenuRequest( 
                        ( new VisitorActualMenuMessage( visitorMenu ) ) ), visitorId );
                    _menuAgent.AddVisitorSubscriber( visitorId );
                    break;
                case ( VisitorAdminActionTypes.MAKE_ORDER ):
                    List<DishAgent> dishes = new List<DishAgent>();
                    foreach ( int selectedDishId in message.SelectedDishesIds )
                    {
                        MenuDish selectedDish = menuDishes.First( md => md.Id == selectedDishId );
                        List<Prod> products = selectedDish.Card.Operations.SelectMany( o => o.Products ).ToList();

                        ProcessAgent processAgent = AgentFabric.ProcessAgentCreate( Id );
                        foreach ( DFs.DFDishCards.Operation oper in selectedDish.Card.Operations )
                        {
                            OperationAgent operationAgent = AgentFabric.OperationAgentCreate( 
                                oper.Type,
                                oper.EquipType,
                                oper.Time,
                                selectedDish.CardId,
                                processAgent.Id,
                                Id );

                            processAgent.AddOperationAgent( operationAgent );
                        }
                        DishAgent dishAgent = AgentFabric.DishAgentCreate( processAgent, products, Id );

                        dishes.Add( dishAgent );
                    }
                    OrderAgent orderAgent = AgentFabric.OrderAgentCreate( dishes, visitorId, Id );
                    //SendMessageToAgent<string>( orderAgent.Id, visitorId );
                    break;
            }
        }
    }
}
