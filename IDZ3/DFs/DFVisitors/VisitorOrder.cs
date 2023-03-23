using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFVisitors
{
    public class VisitorOrder
    {
        [JsonPropertyName( "vis_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "vis_ord_started" )]
        public string Started { get; set; }

        [JsonPropertyName( "vis_ord_ended" )]
        public string Ended { get; set; }

        [JsonPropertyName( "vis_ord_total" )]
        public double Total { get; set; }

        [JsonPropertyName( "vis_ord_dishes" )]
        public List<OrdDish> OrdDishes { get; set; }

        public VisitorOrder( string name, string started, string ended, double total, List<OrdDish> ordDishes )
        {
            Name = name;
            Started = started;
            Ended = ended;
            Total = total;
            OrdDishes = ordDishes;
        }
    }
}
