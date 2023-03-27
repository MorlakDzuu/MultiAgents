using IDZ3.Agents.Admin;
using IDZ3.Agents.Visitor;
using IDZ3.DFs.DFVisitors;
using IDZ3.Services.AgentFabric;
using IDZ3.Services.SourceLogService;

namespace IDZ3
{
    public class CafeBuilder
    {
        public int VisitorGroupsNumber { get; set; }
        public int Interval { get; set; }
        public int AdminExistsTime { get; set; }
        private string _pathOutput;
        private List<VisitorOrder> _visitorModels;
        
        public CafeBuilder( string pathOut )
        {
            _pathOutput = pathOut;
            VisitorGroupsNumber = 1;
            _visitorModels = DFVisitors.GetValue().VisitorsOrders;
        }

        public void BuildAdmin()
        {
            Thread thread = new Thread(() =>
            {
                AdminAgent admin = AgentFabric.AdminAgentCreate();
                Thread.Sleep( AdminExistsTime );
                admin.SelfDestruct();

                LogService.Instance().StoreProcessLogsAsync( _pathOutput + "process_log.txt" );
                LogService.Instance().StoreOperationLogsAsync( _pathOutput + "operation_log.txt" );
            } );
            thread.Start();
        }

        public void BuildVisiors()
        {
            Random rnd = new Random();
            for ( int i = 0; i < VisitorGroupsNumber; i++ )
            {
                foreach ( VisitorOrder visitorOrder in _visitorModels )
                {
                    Thread thread = new Thread( () =>
                     {
                         VisitorAgent visitorAgent = AgentFabric.VisitorAgentCreate( visitorOrder.Name );
                         visitorAgent.GetActualMenu( rnd.NextDouble() * 10 );
                         visitorOrder.OrdDishes.ForEach( d => visitorAgent.AddDishToOrder( d.MenuDish ) );
                         visitorAgent.MakeOrder();
                     } );
                    thread.Start();
                }
                Thread.Sleep( Interval );
            }
        }
    }
}
