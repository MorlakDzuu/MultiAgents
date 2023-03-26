namespace IDZ3.Agents.Operation
{
    public class Operation
    {
        public string? OperId { get; set; }
        public string? OperProcessId { get; set; }
        public int? OperCardId { get; set; }
        public DateTime? OperStarted { get; set; }
        public DateTime? OperEnded { get; set; }
        public int? OperEquipId { get; set; }
        public int? OperCookerId { get; set; }
        public bool OperActive { get; set; }
    }
}
