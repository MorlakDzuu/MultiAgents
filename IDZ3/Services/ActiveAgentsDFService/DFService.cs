using IDZ3.Agents.Base;
using IDZ3.Services.SourceLogService;

namespace IDZ3.Services.ActiveAgentsDFService
{
    public class DFService
    {
        public static DFService dbServiceInstance;

        private List<AgentProfile> activeAgentsInfo;
        private List<IAgent> agenInstances;
        private object locker;

        private LogService loogger = LogService.Instance();

        protected DFService()
        {
            activeAgentsInfo = new List<AgentProfile>();
            agenInstances = new List<IAgent>();
            locker = new object();
        }

        /// <summary>
        /// Зарегестрировать агента
        /// </summary>
        public void RegisterAgent( AgentProfile agentDescription )
        {
            lock( locker )
            {
                if ( activeAgentsInfo.Exists( ad => ad.Id == agentDescription.Id ) )
                {
                    activeAgentsInfo.Add( agentDescription );
                }

                loogger.LogInfo( $"DFService: user {agentDescription.ServiceType} registered" );
            }
        }

        /// <summary>
        /// Удалить записи об агенте
        /// </summary>
        public void DeregesterAgent( AgentProfile agentDescription )
        {
            lock ( locker )
            {
                if( activeAgentsInfo.Exists( ad => ad.Id == agentDescription.Id ) )
                {
                    AgentProfile dFAgent = activeAgentsInfo.First( ad => ad.Id == agentDescription.Id );
                    activeAgentsInfo.Remove( dFAgent );

                    IAgent agent = agenInstances.First( ai => ai.GetId() == agentDescription.Id );
                    agenInstances.Remove( agent );

                    loogger.LogInfo( $"DFService: user {agentDescription.ServiceType} removed" );
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
                agent = agenInstances.FirstOrDefault( ai => ai.GetId() == agentId );
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
