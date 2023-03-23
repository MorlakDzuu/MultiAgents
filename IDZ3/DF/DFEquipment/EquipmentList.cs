using System.Text.Json.Serialization;

namespace IDZ3.DF.DFEquipment
{
    public class EquipmentList
    {
        [JsonPropertyName( "equipment" )]
        public List<Equip> Equipment { get; set; }

        public EquipmentList( List<Equip> equipment )
        {
            Equipment = equipment;
        }
    }
}
