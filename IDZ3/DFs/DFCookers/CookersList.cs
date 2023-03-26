using System.Text.Json.Serialization;

namespace IDZ3.DFs.DFCookers
{
    public class CookersList
    {
        [JsonPropertyName( "cookers" )]
        public List<CookerRes> Cookers { get; set; }

        public CookersList( List<CookerRes> cookers )
        {
            Cookers = cookers;
        }
    }
}
