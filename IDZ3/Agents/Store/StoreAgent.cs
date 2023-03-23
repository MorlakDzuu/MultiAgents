using IDZ3.Agents.Base;
using IDZ3.Agents.Product;
using IDZ3.Message;
using IDZ3.Services.AgentFabric;

namespace IDZ3.Agents.Store
{
    /// <summary>
    /// Агент склада продуктов
    /// </summary>
    public class StoreAgent : BaseAgent
    {
        // Id блюда - Продукт
        private Dictionary<string, List<ProductAgent>> activeProductAgents;
        // Тип продукта - объем остатка
        private Dictionary<string, double> lessProductAmounts;

        public StoreAgent( string name, string ownerId, Dictionary<string, double> reserves ) : base( name, ownerId )
        {
            activeProductAgents = new Dictionary<string, List<ProductAgent>>();
            lessProductAmounts = reserves;
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

                    if ( lessAmount >= recievedMessage.MessageContent.ProductAmount )
                    {
                        SendMessageToAgent<bool>( true, recievedMessage.AgentFromId );
                    }
                    else
                    {
                        SendMessageToAgent<bool>( false, recievedMessage.AgentFromId );
                    }

                    break;

                // Пришел запрос на резервирование продукта
                case ( StoreActionTypes.RESERVE_PRODUCT ):
                    ProductAgent productAgent = AgentFabric.ProductAgentCreate(
                        Id,
                        recievedMessage.MessageContent.ProductType,
                        recievedMessage.MessageContent.ProductAmount );

                    lessProductAmounts[ productAgent.GetType() ] -= productAgent.GetAmount();
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
                        pa => pa.GetType() == recievedMessage.MessageContent.ProductType 
                    );

                    activeProductAgents[ recievedMessage.MessageContent.DishAgentId ].Remove( cancalledProduct );
                    lessProductAmounts[ recievedMessage.MessageContent.ProductType ] += recievedMessage.MessageContent.ProductAmount;
                    cancalledProduct.SelfDestruct();

                    break;
                }
            Unlock();
        }
    }
}
