namespace IDZ3.Services.ActiveAgentsDFService
{
    public class AgentProfile
    {
        public string ServiceType { get; set; }
        public string Id { get; set; }
        public string Ownerwhip { get; set; }

        public DateTime CreationDate = DateTime.UtcNow;

        public AgentProfile(string serviceType, string id, string ownerwhip) {
            ServiceType = serviceType;
            Id = id;
            Ownerwhip = ownerwhip;  
        }

        public static AgentProfile Create( string serviceType, string id, string ownerwhip ) =>  new AgentProfile( serviceType, id, ownerwhip);
    }
}
