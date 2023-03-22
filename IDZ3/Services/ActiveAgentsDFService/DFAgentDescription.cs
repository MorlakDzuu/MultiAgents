namespace IDZ3.Services.ActiveAgentsDFService
{
    public class DFAgentDescription
    {
        public DFServiceDescription ServiceDescription { get; private set; }
        public string AgentId { get; private set; }

        public DFAgentDescription( DFServiceDescription dFServiceDescription, string agentId )
        {
            ServiceDescription = dFServiceDescription;
            AgentId = agentId;
        }
    }
}
