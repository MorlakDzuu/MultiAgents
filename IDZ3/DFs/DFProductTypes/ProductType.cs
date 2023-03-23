using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFProductTypes
{
    public class ProductType
    {
        [JsonPropertyName( "prod_type_id" )]
        public int Id { get; set; }
        
        [JsonPropertyName( "prod_type_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "prod_is_food" )]
        public bool Active { get; set; }

        public ProductType( int id, string name, bool active )
        {
            Id = id;
            Name = name;
            Active = active;
        }
    }
}
