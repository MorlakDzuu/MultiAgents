namespace IDZ3.Agents.Process
{
    public class Process
    {
        public string? ProcessId { get; set; }
        public string? OrderDishId { get; set; }
        public DateTime? ProcessStarted { get; set; }
        public DateTime? ProcessEnded { get; set; }
        public bool ProcessActive { get; set; }
        public List<string>? ProcessOperations { get; set; }
    }
}
