namespace Domain.Contracts;

public class SatelliteSplit
{
    [JsonProperty("distance")]
    public double Distance { get; set; }

    [JsonProperty("message")]
    public List<string> Message { get; set; }
}

