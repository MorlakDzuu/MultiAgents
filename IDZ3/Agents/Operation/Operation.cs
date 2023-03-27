using System.Text.Json.Serialization;

namespace IDZ3.Agents.Operation
{
    public class Operation
    {
        [JsonPropertyName( "oper_id" )]
        public string? OperId { get; set; }

        [JsonPropertyName( "oper_proc" )]
        public string? OperProcessId { get; set; }

        [JsonPropertyName( "oper_card" )]
        public int? OperCardId { get; set; }

        [JsonPropertyName( "oper_started" )]
        public DateTime? OperStarted { get; set; }

        [JsonPropertyName( "oper_ended" )]
        public DateTime? OperEnded { get; set; }

        [JsonPropertyName( "oper_equip_id" )]
        public int? OperEquipId { get; set; }

        [JsonPropertyName( "oper_coocker_id" )]
        public int? OperCookerId { get; set; }

        [JsonPropertyName( "oper_active" )]
        public bool OperActive { get; set; }
    }
}
