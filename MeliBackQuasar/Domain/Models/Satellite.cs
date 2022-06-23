namespace Domain.Models;

public class Satellite
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("distance")]
    public double Distance { get; set; }

    [JsonProperty("message")]
    public List<string> Message { get; set; }
}

