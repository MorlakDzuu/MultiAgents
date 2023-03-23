using System.Text.Json.Serialization;

namespace IDZ3.DF.DFEquipmentType
{
    public class EquipmentTypeList
    {
        [JsonPropertyName( "equipment_type" )]
        public List<EquipType> EquipmentType { get; set; }

        public EquipmentTypeList( List<EquipType> equipmentType )
        {
            Equipment = equipment;
        }
    }
}
