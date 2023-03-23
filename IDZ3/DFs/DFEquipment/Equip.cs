using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFEquipment
{
    public class Oper
    {
        [JsonPropertyName( "equip_type" )]
        public int Type { get; set; }
        
        [JsonPropertyName( "equip_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "equip_active" )]
        public bool Active { get; set; }

        public Oper( int type, string name, bool active )
        {
            Type = type;
            Name = name;
            Active = active;
        }
    }
}
