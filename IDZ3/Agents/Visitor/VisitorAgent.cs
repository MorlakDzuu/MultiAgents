using IDZ3.Agents.Base;
using IDZ3.DFs.DFMenu;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Visitor;
using IDZ3.MessagesContracts;
using System.Text.Json;

namespace IDZ3.Agents.Visitor
{
    /// <summary>
    /// Агент посетителя
    /// </summary>
    public class VisitorAgent : BaseAgent
    {
        // Id управляющего агента
        private string _adminAgentId;
        private List<int> _choosenDishes;

        public double TimeToWait { get; protected set; }
        public List<VisitorMenuDish> Menu { get; protected set; }

        public VisitorAgent( string ownerId ) : base( AgentRoles.VISITOR.ToString(), ownerId )
        {
            _adminAgentId = _dFService.GetFirstId( AgentRoles.ADMIN.ToString() );
            Menu = new List<VisitorMenuDish>();
            _choosenDishes = new List<int>();
        }

        public new void Action()
        {
            Lock();
            Message<VisitorRecieveMessage> message = GetMessage<VisitorRecieveMessage>();
            switch ( message.MessageContent.ActionType )
            {
                case VisitorActionTypes.ASK_ACTUAL_MENU:
                    VisitorGetActualMenuMessage messageContent = 
                        JsonSerializer.Deserialize<VisitorGetActualMenuMessage>( message.MessageContent.SerializedData );
                    SendMessageToAgent<AdminMessage>( 
                        AdminMessage.VisitorCreateAdminMassege( VisitorAdminMessage.CreateMenuRequestMessage( messageContent.TimeLimit ) ), 
                        _adminAgentId );
                    break;

                case VisitorActionTypes.GET_ACTUAL_MENU:
                    VisitorActualMenuMessage messageMenuContent = 
                        JsonSerializer.Deserialize<VisitorActualMenuMessage>( message.MessageContent.SerializedData );
                    Menu = messageMenuContent.Menu;
                    break;

                case VisitorActionTypes.UPDATE_MENU:
                    VisitorMenuUpdatesMessage menuUpdatesMessage = 
                        JsonSerializer.Deserialize<VisitorMenuUpdatesMessage>( message.MessageContent.SerializedData );
                    foreach ( MenuDish updatedDish in menuUpdatesMessage.UpdatedDishes )
                    {
                        if ( Menu.Any( md => md.Id == updatedDish.Id ) )
                        {
                            Menu.First( md => md.Id == updatedDish.Id ).Active = updatedDish.Active;
                        }
                    }
                    break;

                case VisitorActionTypes.ADD_DISH_TO_ORDER:
                    int addDishId = int.Parse( message.MessageContent.SerializedData );
                    _choosenDishes.Add( addDishId );
                    break;

                case VisitorActionTypes.REMOVE_DISH_FROM_ORDER:
                    int removeDishId = int.Parse( message.MessageContent.SerializedData );
                    _choosenDishes.Remove( removeDishId );
                    break;

                case VisitorActionTypes.ASK_ADMIN_CREATE_ORDER:
                    if ( _choosenDishes.Count == 0 )
                    {
                        return;
                    }
                    VisitorAdminMessage visitorMessage = VisitorAdminMessage.CreateMakeOrderMessage( _choosenDishes );
                    SendMessageToAgent<AdminMessage>(
                        AdminMessage.VisitorCreateAdminMassege( visitorMessage ), _adminAgentId );
                    break;

                case VisitorActionTypes.GET_ORDER_WAIT_TIME:
                    VisitorActualOrderWaitTimeMessage waitTimeMessage =
                        JsonSerializer.Deserialize<VisitorActualOrderWaitTimeMessage>( message.MessageContent.SerializedData );
                    TimeToWait = waitTimeMessage.WaitTime;
                    _loogger.LogInfo( $"Order wait time {TimeToWait}" );
                    break;
            }
            Unlock();
        }

        public void GetActualMenu( double timeLimit )
        {
            Lock();
            SendMessageToAgent<VisitorRecieveMessage>( VisitorRecieveMessage.CreateVisitorGetMenuRequest( new VisitorGetActualMenuMessage( timeLimit ) ), Id );
            Unlock();
        }

        public void AddDishToOrder( int menuDishId )
        {
            Lock();
            SendMessageToAgent<VisitorRecieveMessage>( VisitorRecieveMessage.CreateVisitorAddToOrderRequest( menuDishId ), Id );
            Unlock();
        }

        public void RemoveDishFromOrder( int menuDishId )
        {
            Lock();
            SendMessageToAgent<VisitorRecieveMessage>( VisitorRecieveMessage.CreateVisitorRemoveFromOrderRequest( menuDishId ), Id );
            Unlock();
        }

        public void MakeOrder()
        {
            Lock();
            SendMessageToAgent<VisitorRecieveMessage>( VisitorRecieveMessage.CreateVisitorMakeOrderRequest(), Id );
            Unlock();
        }
    }
}
