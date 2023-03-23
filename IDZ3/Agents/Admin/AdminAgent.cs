using IDZ3.Agents.Base;
using IDZ3.Agents.Store;
using IDZ3.MessageContracts.Visitor;
using IDZ3.MessagesContracts;
using IDZ3.Services.AgentFabric;

namespace IDZ3.Agents.Admin
{
    /// <summary>
    /// Управляющий агент
    /// </summary>
    public class AdminAgent : BaseAgent
    {
        // Агент склада, принадлежащий управляющему агенту
        StoreAgent _storeAgent;

        public AdminAgent() : base( AgentRoles.ADMIN.ToString(), "head" )
        {
            // Создаем агент склада
            _storeAgent = AgentFabric.StoreAgentCreate( Id );
        }

        /// <summary>
        /// Поведение управляющего агента
        /// </summary>
        new public void Action()
        {
            Lock();
            Message<VisitorMessage> visitorMessage = GetMessage<VisitorMessage>();
            StopWorking();
            Unlock();
        }

        private void ProcessVisitorRequest( VisitorMessage message )
        {
            switch(message.ActionType)
            {
                case ( VisitorActionTypes.MENU_REQUEST ):
                    break;
            }
        }
    }
}
