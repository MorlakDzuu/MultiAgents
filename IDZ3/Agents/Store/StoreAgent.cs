using IDZ3.Agents.Base;
using IDZ3.Agents.Menu;
using IDZ3.Agents.Product;
using IDZ3.MessageContracts.Admin;
using IDZ3.MessageContracts.Store;
using IDZ3.MessagesContracts;
using IDZ3.Services.AgentFabric;

namespace IDZ3.Agents.Store
{
    /// <summary>
    /// Агент склада продуктов
    /// </summary>
    public class StoreAgent : BaseAgent
    {
        private MenuAgent _menuAgent;
        // Id блюда - Продукт
        private Dictionary<string, List<ProductAgent>> activeProductAgents;
        // Тип продукта - объем остатка
        private Dictionary<int, double> lessProductAmounts;

        public StoreAgent( string ownerId, MenuAgent menuAgent, List<DFs.DFProducts.Product> products ) : base( AgentRoles.STORE.ToString() , ownerId )
        {
            _menuAgent = menuAgent;
            activeProductAgents = new Dictionary<string, List<ProductAgent>>();
            lessProductAmounts = new Dictionary<int, double>();
            products.ForEach( product => lessProductAmounts.Add( product.Type, product.Quantity ) );
            _menuAgent.UpdateProductsStore( lessProductAmounts );
        }

        /// <summary>
        /// Поведение склада
        /// </summary>
        new public void Action()
        {
            Lock();

            // Ждем новых запросов
            Message<StoreRecieveMessage> recievedMessage = GetMessage<StoreRecieveMessage>();

            switch ( recievedMessage.MessageContent.ActionType ) {

                // Пришел запрос на наличие продукта
                case ( StoreActionTypes.CHECK_PRODUCT ):
                    double lessAmount = lessProductAmounts[ recievedMessage.MessageContent.ProductType ];

                    StoreCheckResultMessage storeCheckResultMessage = StoreCheckResultMessage.Create(
                        recievedMessage.MessageContent.ProductType,
                        recievedMessage.MessageContent.ProductAmount,
                        recievedMessage.MessageContent.DishAgentId,
                        recievedMessage.MessageContent.OrderAgentId,
                        false );

                    if ( lessAmount >= recievedMessage.MessageContent.ProductAmount )
                    {
                        storeCheckResultMessage.Result = true;
                    }

                    SendMessageToAgent<AdminMessage>( AdminMessage.StoreCreateAdminMassege( storeCheckResultMessage ), recievedMessage.AgentFromId );
                    break;

                // Пришел запрос на резервирование продукта
                case ( StoreActionTypes.RESERVE_PRODUCT ):
                    ProductAgent productAgent = AgentFabric.ProductAgentCreate(
                        Id,
                        recievedMessage.MessageContent.DishAgentId,
                        recievedMessage.MessageContent.OrderAgentId,
                        recievedMessage.MessageContent.ProductType );

                    if ( lessProductAmounts[ productAgent.GetProductType() ] < recievedMessage.MessageContent.ProductAmount )
                    {
                        productAgent.Reserve();
                        break;
                    }

                    productAgent.SetAmount( recievedMessage.MessageContent.ProductAmount );
                    productAgent.Reserve();
                    lessProductAmounts[ productAgent.GetProductType() ] -= productAgent.GetAmount();

                    _menuAgent.UpdateProductsStore( lessProductAmounts );

                    if ( !activeProductAgents.ContainsKey( recievedMessage.MessageContent.DishAgentId ) )
                    {
                        activeProductAgents.Add(
                            recievedMessage.MessageContent.DishAgentId,
                            new List<ProductAgent>() );
                    }

                    activeProductAgents[ recievedMessage.MessageContent.DishAgentId ].Add( productAgent );
                    break;

                // Блюдо готово!
                case ( StoreActionTypes.DISH_READY ):
                    activeProductAgents.Remove( recievedMessage.MessageContent.DishAgentId );
                    break;

                // Отмена бронирования продукта
                case ( StoreActionTypes.CANCEL_PRODUCT ):
                    ProductAgent cancalledProduct = activeProductAgents[ recievedMessage.MessageContent.DishAgentId ].First(
                        pa => pa.GetProductType() == recievedMessage.MessageContent.ProductType 
                    );

                    activeProductAgents[ recievedMessage.MessageContent.DishAgentId ].Remove( cancalledProduct );
                    lessProductAmounts[ recievedMessage.MessageContent.ProductType ] += recievedMessage.MessageContent.ProductAmount;
                    cancalledProduct.SelfDestruct();

                    _menuAgent.UpdateProductsStore( lessProductAmounts );
                    break;
                }
            Unlock();
        }

        public new void SelfDiscruct()
        {
            activeProductAgents.Values.ToList().SelectMany( p => p ).ToList().ForEach( p => p.SelfDestruct() );
            activeProductAgents.Clear();
            base.SelfDestruct();
        }
    }
}
