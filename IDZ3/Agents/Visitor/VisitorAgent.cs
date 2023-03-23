using IDZ3.Agents.Base;
using IDZ3.MessageContracts.Visitor;

namespace IDZ3.Agents.Visitor
{
    /// <summary>
    /// Агент посетителя
    /// </summary>
    public class VisitorAgent : BaseAgent
    {
        // Id управляющего агента
        private string _adminAgentId;

        public VisitorAgent( string ownerId ) : base( AgentRoles.VISITOR.ToString(), ownerId )
        {
            _adminAgentId = String.Empty;
        }

        public void Action()
        {
            Lock();
            if ( String.IsNullOrEmpty( _adminAgentId ) )
            {
               _adminAgentId = _dFService.GetFirstId( AgentRoles.ADMIN.ToString() );   
            }
            SendMessageToAgent<VisitorMessage>( VisitorMessage.CreateMenuRequestMessage( 20 ), _adminAgentId );

        }
    }
}
