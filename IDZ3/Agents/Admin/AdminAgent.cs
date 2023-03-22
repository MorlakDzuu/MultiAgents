using IDZ3.Agents.Base;
using IDZ3.Agents.Store;
using IDZ3.Message;
using IDZ3.Services.AgentFabric;

namespace IDZ3.Agents.Admin
{
    public class AdminAgent : BaseAgent
    {
        StoreAgent _storeAgent;

        public AdminAgent() : base( "ADMIN", "head" )
        {
            _storeAgent = AgentFabric.StoreAgentCreate( Id );
        }

        new public void Action()
        {
            Lock();

            StoreRecieveMessage messageContent = new StoreRecieveMessage( StoreActionTypes.CHECK_PRODUCT, "CARROT", 1, "test" );
            SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );

            Message<bool> checkResult = GetMessage<bool>();
            if ( checkResult.MessageContent )
            {
                messageContent.ActionType = StoreActionTypes.RESERVE_PRODUCT;
                SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );
            }

            messageContent.ActionType = StoreActionTypes.CANCEL_PRODUCT;
            SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );

            StopWorking();
            Unlock();
        }
    }
}
