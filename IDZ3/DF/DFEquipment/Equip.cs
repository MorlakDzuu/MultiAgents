using System.Text.Json.Serialization;

namespace IDZ3.DF.DFEquipment
{
    public class Equip
    {
        [JsonPropertyName( "equip_type" )]
        public int Type { get; set; }
        
        [JsonPropertyName( "equip_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "equip_active" )]
        public bool Active { get; set; }

        public Equip(int type, string name, bool active)
        {
            Type = type;
            Name = name;
            Active = active;
        }
    }
}
