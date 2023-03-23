using System.Text.Json.Serialization;

namespace IDZ3.DF.DFEquipmentType
{
    public class EquipType
    {
        [JsonPropertyName( "equip_type_id" )]
        public int Id { get; set; }
        
        [JsonPropertyName( "equip_type_name" )]
        public string Name { get; set; }

        public EquipType( int id, string name )
        {
            Id = id;
            Name = name;
        }
    }
}
