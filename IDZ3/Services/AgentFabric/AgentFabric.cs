using IDZ3.Agents;
using IDZ3.Agents.Admin;
using IDZ3.Agents.Base;
using IDZ3.Agents.Store;

namespace IDZ3.Services.AgentFabric
{
    public static class AgentFabric
    {
        public static BaseAgent BaseAgentCreate(
            string name,
            string ownerId )
        {
            BaseAgent baseAgent = new BaseAgent( name, ownerId );
            Thread thread = new Thread( ( BaseAgent ) =>
            {
                while( !baseAgent.Done() )
                {
                    baseAgent.Action();
                }
            } );

            thread.Start( baseAgent );

            return baseAgent;
        }

        public static AdminAgent AdminAgentCreate()
        {
            AdminAgent adminAgent = new AdminAgent();

            Thread thread = new Thread( ( BaseAgent ) =>
            {
                while ( !adminAgent.Done() )
                {
                    adminAgent.Action();
                }
            } );

            thread.Start( adminAgent );

            return adminAgent;
        }
             
        // TODO: reserves from file
        public static StoreAgent StoreAgentCreate(
            string ownerId
            )
        {
            // ------
            Dictionary<string, double> reserves = new Dictionary<string, double>();
            reserves.Add( "POTATO", 2.3 );
            reserves.Add( "CARROT", 1.4 );
            reserves.Add( "TEA", 10 );
            // -------

            StoreAgent storeAgent = new StoreAgent( "STORE", ownerId, reserves );

            Thread thread = new Thread( ( StoreAgent ) =>
            {
                while ( !storeAgent.Done() )
                {
                    storeAgent.Action();
                }
            } );

            thread.Start( storeAgent );

            return storeAgent;
        }

        public static ProductAgent ProductAgentCreate(
            string ownerId,
            string type,
            double amount )
        {
            ProductAgent productAgent = new ProductAgent( ownerId, type, amount );
            Thread thread = new Thread( ( ProductAgent ) =>
            {
                while ( !productAgent.Done() )
                {
                    productAgent.Action();
                }
            } );

            thread.Start();

            return productAgent;
        }
    }
}
