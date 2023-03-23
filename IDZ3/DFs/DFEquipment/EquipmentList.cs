using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFEquipment
{
    public class EquipmentList
    {
        [JsonPropertyName( "equipment" )]
        public List<Oper> Equipment { get; set; }

        public EquipmentList( List<Oper> equipment )
        {
            Equipment = equipment;
        }
    }
}
