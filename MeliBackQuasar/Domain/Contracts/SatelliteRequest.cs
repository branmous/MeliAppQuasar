namespace Domain.Contracts
{
    public class SatelliteRequest
    {
        [JsonProperty("satellites")]
        public List<Satellite> Satellites { get; set; }
    }
}

