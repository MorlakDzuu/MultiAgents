using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFEquipmentType
{
    public class EquipmentType
    {
        [JsonPropertyName( "equip_type_id" )]
        public int Id { get; set; }
        
        [JsonPropertyName( "equip_type_name" )]
        public string Name { get; set; }

        public EquipmentType( int id, string name )
        {
            Id = id;
            Name = name;
        }
    }
}
