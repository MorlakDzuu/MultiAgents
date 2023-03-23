using System.Text.Json.Serialization;

namespace IDZ3.DF.DFCookers
{
    public class Cooker
    {
        [JsonPropertyName( "cook_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "cook_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "cook_active" )]
        public bool Active { get; set; }

        public Cooker( int id, string name, bool active )
        {
            Id = id;
            Name = name;
            Active = active;
        }
    }
}
