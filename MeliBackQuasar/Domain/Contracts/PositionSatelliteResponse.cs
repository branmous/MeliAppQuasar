namespace Domain.Contracts;

public class PositionSatelliteResponse
{
    [JsonProperty("position")]
    public Position Position { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

