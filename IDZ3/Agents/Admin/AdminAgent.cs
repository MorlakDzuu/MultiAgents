using IDZ3.Agents.Base;
using IDZ3.Agents.Store;
using IDZ3.Message;
using IDZ3.Services.AgentFabric;

namespace IDZ3.Agents.Admin
{
    // Управляющий агент
    public class AdminAgent : BaseAgent
    {
        // Агент склада, принадлежащий управляющему агенту
        StoreAgent _storeAgent;

        public AdminAgent() : base( "ADMIN", "head" )
        {
            // Создаем агент склада
            _storeAgent = AgentFabric.StoreAgentCreate( Id );
        }

        // Поведение управляющего агента
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

            messageContent.ActionType = StoreActionTypes.RESERVE_PRODUCT;
            SendMessageToAgent<StoreRecieveMessage>( messageContent, _storeAgent.Id );

            StoreRecieveMessage reserseTea = new StoreRecieveMessage( StoreActionTypes.RESERVE_PRODUCT, "TEA", 2, "test" );
            SendMessageToAgent<StoreRecieveMessage>( reserseTea, _storeAgent.Id );

            StoreRecieveMessage ready = new StoreRecieveMessage( StoreActionTypes.DISH_READY, "", 0, "test" );
            SendMessageToAgent<StoreRecieveMessage>( ready, _storeAgent.Id );

            StopWorking();
            Unlock();
        }
    }
}
