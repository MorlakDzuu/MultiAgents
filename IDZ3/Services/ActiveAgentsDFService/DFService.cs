using IDZ3.Agents.Base;
using IDZ3.Services.SourceLogService;

namespace IDZ3.Services.ActiveAgentsDFService
{
    public class DFService
    {
        public static DFService dbServiceInstance;

        private List<AgentProfile> activeAgentsInfo;
        private Dictionary<string, IAgent> agenInstances;
        private object locker;

        private LogService loogger = LogService.Instance();

        protected DFService()
        {
            activeAgentsInfo = new List<AgentProfile>();
            agenInstances = new Dictionary<string, IAgent>();
            locker = new object();
        }

        /// <summary>
        /// Зарегестрировать агента
        /// </summary>
        public void RegisterAgent( AgentProfile agentDescription, IAgent agent )
        {
            lock( locker )
            {
                if ( !activeAgentsInfo.Exists( ad => ad.Id == agentDescription.Id ) )
                {
                    activeAgentsInfo.Add( agentDescription );
                    agenInstances.Add( agentDescription.Id, agent );

                    loogger.LogInfo( $"DFService: user {agentDescription.ServiceType} registered" );
                }
            }
        }

        /// <summary>
        /// Удалить записи об агенте
        /// </summary>
        public void DeregesterAgent( string agentId )
        {
            lock ( locker )
            {
                if( activeAgentsInfo.Exists( ad => ad.Id == agentId ) )
                {
                    AgentProfile dFAgent = activeAgentsInfo.First( ad => ad.Id == agentId );
                    activeAgentsInfo.Remove( dFAgent );

                    agenInstances.Remove( agentId );

                    loogger.LogInfo( $"DFService: {dFAgent.ServiceType} {agentId} removed" );
                }
            }
        }

        /// <summary>
        /// Получить агента
        /// </summary>
        public IAgent? GetAgentById( string agentId )
        {
            IAgent agent;
            lock( locker )
            {
                agent = agenInstances[agentId];
            }

            return agent;
        }

        public string GetFirstId( string serviceType )
        {
            return activeAgentsInfo.FirstOrDefault( ai => ai.ServiceType == serviceType ).Id;
        }

        public static DFService GetInstance()
        {
            if ( dbServiceInstance == null )
            {
                dbServiceInstance = new DFService();
            }

            return dbServiceInstance;
        }
    }
}
