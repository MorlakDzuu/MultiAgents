using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFVisitors
{
    public class VisitorOrderList
    {
        [JsonPropertyName( "visitors_orders" )]
        public List<VisitorOrder> VisitorsOrders { get; set; }

        public VisitorOrderList( List<VisitorOrder> visitorsOrders )
        {
            VisitorsOrders = visitorsOrders;
        }
    }
}
