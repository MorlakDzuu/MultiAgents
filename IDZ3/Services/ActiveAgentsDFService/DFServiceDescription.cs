namespace IDZ3.Services.ActiveAgentsDFService
{
    public class DFServiceDescription
    {
        public string ServiceType { get; private set; }
        public string Ownership { get; private set; }

        public DFServiceDescription( string serviceType, string ownership )
        {
            ServiceType = serviceType;
            Ownership = ownership;
        }
    }
}
