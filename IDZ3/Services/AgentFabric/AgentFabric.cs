﻿using IDZ3.Agents;
using IDZ3.Agents.Base;
using IDZ3.Agents.Store;

namespace IDZ3.Services.AgentFabric
{
    public static class AgentFabric
    {
        public static BaseAgent BaseAgentCreate( string name, string ownerId )
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

        public static StoreAgent StoreAgentCreate( string name, string ownerId )
        {
            StoreAgent storeAgent = new StoreAgent( name, ownerId );
            // TODO: Init store

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

        public static ProductAgent ProductAgentCreate( string ownerId, string type )
        {
            ProductAgent productAgent = new ProductAgent( ownerId, type );
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