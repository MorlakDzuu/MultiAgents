using IDZ3.Agents.Admin;
using IDZ3.Agents.Base;
using IDZ3.Agents.Cooker;
using IDZ3.Agents.Dish;
using IDZ3.Agents.Equipment;
using IDZ3.Agents.Menu;
using IDZ3.Agents.Operation;
using IDZ3.Agents.Order;
using IDZ3.Agents.Process;
using IDZ3.Agents.Product;
using IDZ3.Agents.Store;
using IDZ3.Agents.Visitor;
using IDZ3.DFs.DFCookers;
using IDZ3.DFs.DFDishCards;
using IDZ3.DFs.DFEquipment;
using IDZ3.DFs.DFEquipmentType;
using IDZ3.DFs.DFOperations;

namespace IDZ3.Services.AgentFabric
{
    public static class AgentFabric
    {
        public static BaseAgent BaseAgentCreate(
            string name,
            string ownerId )
        {
            BaseAgent baseAgent = new BaseAgent( name, ownerId );
            Base agent = new Base( baseAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return baseAgent;
        }

        record class Base( BaseAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static AdminAgent AdminAgentCreate()
        {
            AdminAgent adminAgent = new AdminAgent();
            Admin agent = new Admin( adminAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return adminAgent;
        }

        record class Admin( AdminAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static MenuAgent MenuAgentCreate( string ownerId )
        {
            MenuAgent menuAgent = new MenuAgent( ownerId );

            Menu agent = new Menu( menuAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return menuAgent;
        }

        record class Menu( MenuAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        // TODO: reserves from file
        public static StoreAgent StoreAgentCreate(
            string ownerId,
            MenuAgent menuAgent,
            List<DFs.DFProducts.Product> reserves
            )
        {
            StoreAgent storeAgent = new StoreAgent( ownerId, menuAgent, reserves );

            Store agent = new Store( storeAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return storeAgent;
        }

        record class Store( StoreAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static ProductAgent ProductAgentCreate(
            string ownerId,
            string dishAgentId,
            string orderAgentId,
            int type )
        {
            ProductAgent productAgent = new ProductAgent( ownerId, dishAgentId, orderAgentId, type );

            Product agent = new Product( productAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return productAgent;
        }

        record class Product( ProductAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static VisitorAgent VisitorAgentCreate( string ownerId )
        {
            VisitorAgent visitorAgent = new VisitorAgent( ownerId );
            
            Visitor agent = new Visitor( visitorAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return visitorAgent;
        }

        record class Visitor( VisitorAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static CookerAgent CookerAgentCreate(
            CookerRes cookerInfo,
            string ownerId )
        {
            CookerAgent cookerAgent = new CookerAgent( cookerInfo, ownerId );

            Cooker agent = new Cooker( cookerAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return cookerAgent;
        }

        record class Cooker( CookerAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static EquipmentAgent EquipmentAgentCreate(
            Oper equipInfo,
            string ownerId )
        {
            EquipmentAgent equipmentAgent = new EquipmentAgent( equipInfo, ownerId );

            Equipment agent = new Equipment( equipmentAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return equipmentAgent;
        }

        record class Equipment( EquipmentAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static ProcessAgent ProcessAgentCreate(
            string ownerId )
        {
            ProcessAgent processAgent = new ProcessAgent( ownerId );

            Process agent = new Process( processAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return processAgent;
        }

        record class Process( ProcessAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static OperationAgent OperationAgentCreate(
            int operationType,
            int equipmentType,
            double time,
            int cardId,
            string processId,
            string ownerId )
        {
            OperationAgent operationAgent = new OperationAgent(
                operationType,
                equipmentType,
                time,
                cardId,
                processId,
                ownerId );

            Operation agent = new Operation( operationAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return operationAgent;
        }

        record class Operation( OperationAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static DishAgent DishAgentCreate(
            ProcessAgent processAgent,
            List<Prod> reservedProducts,
            string ownerId )
        {
            DishAgent dishAgent = new DishAgent( processAgent, reservedProducts, ownerId );

            Dish agent = new Dish( dishAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return dishAgent;
        }

        record class Dish( DishAgent Agent )
        {
            public void Start()
            {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }

        public static OrderAgent OrderAgentCreate(
            List<DishAgent> dishes,
            string visitorAgentId,
            string ownerId )
        {
            OrderAgent orderAgent = new OrderAgent( dishes, visitorAgentId, ownerId );

            Order agent = new Order( orderAgent );
            Thread thread = new Thread( agent.Start );
            thread.Start();

            return orderAgent;
        }

        record class Order( OrderAgent Agent )
        {
            public void Start() {
                do
                {
                    Agent.Action();
                } while ( !Agent.Done() );
            }
        }
    }
}
