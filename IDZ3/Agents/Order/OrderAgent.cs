using IDZ3.Agents.Base;
using IDZ3.Agents.Dish;
using IDZ3.DFs.DFDishCards;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Order;
using IDZ3.MessageContracts.Product;
using IDZ3.MessageContracts.Visitor;
using IDZ3.MessagesContracts;
using System.Text.Json;

namespace IDZ3.Agents.Order
{
    public class OrderAgent : BaseAgent
    {
        private List<DishAgent> _dishAgents;
        private string _visitorAgentId;
        private List<Prod> _productsToReserve;
        private List<string> _reservedProductIds;
        private List<double> _waitTimes;

        public OrderAgent(
            List<DishAgent> dishAgents,
            string visitorAgentId,
            string ownerId ) : base( AgentRoles.ORDER.ToString(), ownerId )
        {
            _waitTimes = new List<double>();
            _dishAgents = dishAgents;
            _visitorAgentId = visitorAgentId;
            _productsToReserve = _dishAgents.SelectMany( d => d.GetProductsList() ).ToList();
            _reservedProductIds = new List<string>();
            ReserveProducts();
        }

        public void Action()
        {
            Lock();
            Message<OrderRecieveMessage> message = GetMessage<OrderRecieveMessage>();
            switch ( message.MessageContent.ActionType )
            {
                case OrderActionTypes.PRODUCT_RESERVE_RESULT:
                    ProductReserveResult productReserveResult = JsonSerializer.Deserialize<ProductReserveResult>( message.MessageContent.SerializedData );
                    if ( productReserveResult.Result )
                    {
                        _dishAgents.First( da => da.Id == productReserveResult.DishAgentId ).AddProductAgentId( message.AgentFromId );
                        _reservedProductIds.Add( message.AgentFromId );
                    } else
                    {

                    }
                    if ( _productsToReserve.Count == _reservedProductIds.Count ) {
                        SendMessageToAgent<OrderRecieveMessage>( OrderRecieveMessage.ProductsReservesMessage(), Id );
                    }

                    break;
                case OrderActionTypes.PRODUCTS_RESERVED:
                    foreach ( DishAgent dishAgentItem in _dishAgents )
                    {
                        dishAgentItem.GetProcessAgent().SetOrderAgentId( Id );
                        dishAgentItem.GetProcessAgent().StartProcess();
                    }

                    break;
                case OrderActionTypes.DISH_IS_READY:
                    OrderDishReadyMessage orderDishReadyMessage = JsonSerializer.Deserialize<OrderDishReadyMessage>( message.MessageContent.SerializedData );
                    DishAgent dishAgent = _dishAgents.First( da => da.Id == orderDishReadyMessage.DishAgentId );
                    _dishAgents.Remove( dishAgent );
                    dishAgent.SelfDestruct();
                    SendMessageToAgent<AdminMessage>( AdminMessage.OrderCreateAdminMessage( 
                        OrderAdminMessage.OrderAdminDishIsReadyMessage( 
                            new OrderDishReadyMessage( orderDishReadyMessage.DishAgentId ) ) ), OwnerId );
                    break;

                case OrderActionTypes.PROCESS_WAIT_TIME:
                    double processWaitTime = double.Parse( message.MessageContent.SerializedData );
                    _waitTimes.Add( processWaitTime );
                    if ( _waitTimes.Count == _dishAgents.Count )
                    {
                        SendMessageToAgent<VisitorRecieveMessage>(
                            VisitorRecieveMessage.CreateVisitorWaitTimeRequest( new VisitorActualOrderWaitTimeMessage( _waitTimes.Max() ) ),
                            _visitorAgentId );
                    }
                    break;
            }
            Unlock();
        }

        private void ReserveProducts()
        {
            foreach ( DishAgent dishAgent in _dishAgents )
            {
                foreach ( Prod prod in dishAgent.GetProductsList() )
                {
                    SendMessageToAgent<AdminMessage>(
                        AdminMessage.OrderCreateAdminMessage( OrderAdminMessage.OrderAdminReserveProductMessage( 
                            OrderReserveProductMessage.Create( prod.Type, prod.Quantity, dishAgent.Id ) ) ),
                        OwnerId );
                }
            }
        }
    }
}
