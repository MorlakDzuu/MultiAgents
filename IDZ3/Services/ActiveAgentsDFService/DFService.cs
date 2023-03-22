using IDZ3.Agents.Base;

namespace IDZ3.Services.ActiveAgentsDFService
{
    public class DFService
    {
        public static DFService dbServiceInstance;

        private List<DFAgentDescription> activeAgentsInfo;
        private List<IAgent> agenInstances;
        private object locker;

        protected DFService()
        {
            activeAgentsInfo = new List<DFAgentDescription>();
            agenInstances = new List<IAgent>();
            locker = new object();
        }

        /// <summary>
        /// Зарегестрировать агента
        /// </summary>
        public void RegisterAgent( DFAgentDescription agentDescription )
        {
            lock( locker )
            {
                if ( activeAgentsInfo.Exists( ad => ad.AgentId == agentDescription.AgentId ) )
                {
                    activeAgentsInfo.Add( agentDescription );
                }
            }
        }

        /// <summary>
        /// Удалить записи об агенте
        /// </summary>
        public void DeregesterAgent( DFAgentDescription agentDescription )
        {
            lock ( locker )
            {
                if( activeAgentsInfo.Exists( ad => ad.AgentId == agentDescription.AgentId ) )
                {
                    DFAgentDescription dFAgent = activeAgentsInfo.First( ad => ad.AgentId == agentDescription.AgentId );
                    activeAgentsInfo.Remove( dFAgent );

                    IAgent agent = agenInstances.First( ai => ai.GetId() == agentDescription.AgentId );
                    agenInstances.Remove( agent );
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
