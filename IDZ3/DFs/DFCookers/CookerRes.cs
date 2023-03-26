using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFCookers
{
    public class CookerRes
    {
        [JsonPropertyName( "cook_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "cook_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "cook_active" )]
        public bool Active { get; set; }

        public CookerRes( int id, string name, bool active )
        {
            Id = id;
            Name = name;
            Active = active;
        }
    }
}
