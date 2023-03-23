using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFCookers
{
    public class CookersList
    {
        [JsonPropertyName( "cookers" )]
        public List<Cooker> Cookers { get; set; }

        public CookersList( List<Cooker> cookers )
        {
            Cookers = cookers;
        }
    }
}
