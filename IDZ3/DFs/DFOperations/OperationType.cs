using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFOperations
{
    public class OperationType
    {
        [JsonPropertyName( "oper_type_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "oper_type_name" )]
        public string Name { get; set; }

        public OperationType( int id, string name )
        {
            Id = id;
            Name = name;
        }
    }
}
