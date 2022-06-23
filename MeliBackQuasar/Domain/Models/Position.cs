namespace Domain.Models;

public class Position
{
    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y")]
    public double Y { get; set; }

    [JsonIgnore]
    public double R { get; set; }
}

public class CalculatePosition : Position
{
    public double Distance { get; set; }

    public double Radius { get; set; }
}

