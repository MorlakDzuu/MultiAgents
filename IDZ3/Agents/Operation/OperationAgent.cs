using IDZ3.Agents.Base;
using IDZ3.MessageContracts.Operation;

namespace IDZ3.Agents.Operation
{
    /// <summary>
    /// Агент операции процесса
    /// </summary>
    public class OperationAgent : OneBehaviorBaseAgent
    {
        // Операция процесса
        private Operation _operation; 

        // Id управляющего агента
        private string _adminAgentId;

        public OperationAgent( Operation operation, string ownerId ) 
            : base( AgentRoles.OPERATION.ToString(), ownerId )
        {
            _operation = operation;
            _adminAgentId = String.Empty;
        }

        /// <summary>
        /// Запрашивает у управляющего агента резервирование повара
        /// и кухонного оборудования для выполнеия операции
        /// </summary>
        public new void Action()
        {
            Lock();
            if ( String.IsNullOrEmpty( _adminAgentId ) )
            {
                _adminAgentId = _dFService.GetFirstId( AgentRoles.ADMIN.ToString() );
            }
            SendMessageToAgent<OperationMessage>( OperationMessage.Create( _operation ), _adminAgentId );
            Unlock();
        }
    }
}
