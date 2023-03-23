using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFProducts
{
    public class Product
    {
        [JsonPropertyName( "prod_item_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "prod_item_type" )]
        public int Type { get; set; }
        
        [JsonPropertyName( "prod_item_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "prod_item_company" )]
        public string Company { get; set; }

        [JsonPropertyName( "prod_item_unit" )]
        public string Unit { get; set; }

        [JsonPropertyName( "prod_item_quantity" )]
        public double Quantity { get; set; }

        [JsonPropertyName( "prod_item_cost" )]
        public double Cost { get; set; }

        [JsonPropertyName( "prod_item_delivered" )]
        public string Delivered { get; set; }

        [JsonPropertyName( "prod_item_valid_until" )]
        public string Until { get; set; }

        public Product( 
            int id,
            int type,
            string name,
            string company,
            string unit,
            double quantity,
            double cost,
            string delivered,
            string until )
        {
            Id = id;
            Type = type;
            Name = name;
            Company = company;
            Unit = unit;
            Quantity = quantity;
            Cost = cost;
            Delivered = delivered;
            Until = until;
        }
    }
}
