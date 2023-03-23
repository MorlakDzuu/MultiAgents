using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFDishCards
{
    public class Prod
    {
        [JsonPropertyName( "prod_type" )]
        public int Type { get; set; }
        
        [JsonPropertyName( "prod_quantity" )]
        public double Quantity { get; set; }

        public Prod( int type, double quantity )
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
