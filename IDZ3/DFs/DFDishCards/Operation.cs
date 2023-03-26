using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFDishCards
{
    public class Operation
    {
        [JsonPropertyName( "oper_type" )]
        public int Type { get; set; }

        [JsonPropertyName( "equip_type" )]
        public int EquipType { get; set; }

        [JsonPropertyName( "oper_time" )]
        public double Time { get; set; }

        [JsonPropertyName( "oper_async_point" )]
        public int AsyncPoint { get; set; }

        [JsonPropertyName( "oper_products" )]
        public List<Prod> Products { get; set; }

        public Operation( int type, int equipType, double time, int asyncPoint, List<Prod> products )
        {
            Type = type;
            EquipType = equipType;
            Time = time;
            AsyncPoint = asyncPoint;
            Products = products;
        }
    }
}
