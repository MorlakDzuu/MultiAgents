using System.Text.Json.Serialization;

namespace IDZ3.Agents.Process
{
    public class Process
    {
        [JsonPropertyName( "proc_id" )]
        public string? ProcessId { get; set; }

        [JsonPropertyName( "ord_dish" )]
        public string? OrderDishId { get; set; }

        [JsonPropertyName( "proc_started" )]
        public DateTime? ProcessStarted { get; set; }

        [JsonPropertyName( "proc_ended" )]
        public DateTime? ProcessEnded { get; set; }

        [JsonPropertyName( "proc_active" )]
        public bool ProcessActive { get; set; }

        [JsonPropertyName( "proc_operations" )]
        public List<string>? ProcessOperations { get; set; }
    }
}
