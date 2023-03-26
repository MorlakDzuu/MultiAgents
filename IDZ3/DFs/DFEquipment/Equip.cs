using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFEquipment
{
    public class Oper
    {
        [JsonPropertyName( "equip_id" )]
        public int Id { get; set; }

        [JsonPropertyName( "equip_type" )]
        public int Type { get; set; }
        
        [JsonPropertyName( "equip_name" )]
        public string Name { get; set; }

        [JsonPropertyName( "equip_active" )]
        public bool Active { get; set; }

        public Oper( int id, int type, string name, bool active )
        {
            Id = id;
            Type = type;
            Name = name;
            Active = active;
        }
    }
}
