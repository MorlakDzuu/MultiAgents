using System.Text.Json.Serialization;

namespace IDZ3.DF.DFOperations
{
    public class KithenOperationType
    {
        [JsonPropertyName( "oper_type_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "oper_type_name" )]
        public string Name { get; set; }

        public KithenOperationType( int id, string name )
        {
            Id = id;
            Name = name;
        }
    }
}
