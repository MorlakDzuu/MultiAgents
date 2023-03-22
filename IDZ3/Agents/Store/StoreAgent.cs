using IDZ3.Agents.Base;
using IDZ3.Message;
using System.Collections.Concurrent;

namespace IDZ3.Agents.Store
{
    // Агент склада продуктов
    public class StoreAgent : BaseAgent
    {
        // Id блюда - 
        private ConcurrentDictionary<string, ProductAgent> activeProductAgents;
        private ConcurrentDictionary<string, double> lessProductAmounts;

        public StoreAgent( string name, string ownerId, Dictionary<string, double> reserves ) : base( name, ownerId )
        {
            activeProductAgents = new List<ProductAgent>();
            lessProductAmounts = reserves;
        }

        new public void Action()
        {
            Lock();
            Message<StoreRecieveMessage> recieveMessage = GetMessage<StoreRecieveMessage>();
            double lessAmount;

            switch ( recieveMessage.MessageContent.Type ) {
                case ( StoreActionTypes.CHECK ):
                    lessAmount = lessProductAmounts.GetValueOrDefault( recieveMessage.MessageContent.ProductName );
                    if ( lessAmount > recieveMessage.MessageContent.ProductAmount )
                    {
                        SendMessageToAgent<bool>( true, recieveMessage.AgentFromId );
                    }

                    break;
                case ( StoreActionTypes.RESERVE ):
                    ProductAgent productAgent = new ProductAgent( Id, recieveMessage.MessageContent.ProductName );
                    activeProductAgents.Add( productAgent );
                    break;
                }

            Unlock();
        }
    }
}
