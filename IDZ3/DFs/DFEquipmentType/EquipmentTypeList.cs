using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFEquipmentType
{
    public class EquipmentTypeList
    {
        [JsonPropertyName( "equipment_type" )]
        public List<EquipmentType> EquipmentType { get; set; }

        public EquipmentTypeList( List<EquipmentType> equipmentType )
        {
            EquipmentType = equipmentType;
        }
    }
}
