using Infraestructure.Models;

namespace Domain.Services.Location;

public class LocationService : ILocationService
{
    private readonly IMessageService messageService;
    private readonly ILocationRepository locationRepository;
    private readonly List<List<string>> messages;
    public Position Kenobi = new()
    {
        X = -500,
        Y = -200
    };

    public Position Skywalker = new()
    {
        X = 100,
        Y = -100
    };

    public Position Sato = new()
    {
        X = 500,
        Y = 100
    };


    public LocationService(IMessageService messageService,
        ILocationRepository locationRepository)
    {
        this.messageService = messageService;
        this.locationRepository = locationRepository;
        messages = new List<List<string>>();
    }

    public PositionSatelliteResponse GetLocation(SatelliteRequest satelliteRequest)
    {
        messages.AddRange(satelliteRequest.Satellites.Select(s => s.Message));
        string message = messageService.GetMessage(messages);
        Kenobi.R = satelliteRequest.Satellites.First(p => p.Name.ToLower() == nameof(Kenobi).ToLower()).Distance;
        Skywalker.R = satelliteRequest.Satellites.First(p => p.Name.ToLower() == nameof(Skywalker).ToLower()).Distance;
        Sato.R = satelliteRequest.Satellites.First(p => p.Name.ToLower() == nameof(Sato).ToLower()).Distance;

        Position position = CalculateTrilateration();

        return new PositionSatelliteResponse
        {
            Message = message,
            Position = position
        };
    }

    public PositionSatelliteResponse GetLocation()
    {
        var satellites = locationRepository.GetLocation();

        messages.AddRange(satellites.Select(s => s.Message));
        string message = messageService.GetMessage(messages);

        Kenobi.R = satellites.First(p => p.Name.ToLower() == nameof(Kenobi).ToLower()).Distance;
        Skywalker.R = satellites.First(p => p.Name.ToLower() == nameof(Skywalker).ToLower()).Distance;
        Sato.R = satellites.First(p => p.Name.ToLower() == nameof(Sato).ToLower()).Distance;

        Position position = CalculateTrilateration();

        return new PositionSatelliteResponse()
        {
            Message = message,
            Position = position
        };
    }

    public bool CreateLocation(string name, SatelliteSplit satellite)
    {
        LocationModel location = new()
        {
            Distance = satellite.Distance,
            Message = satellite.Message,
            Name = name,
        };

        return locationRepository.CreateOrUpdate(location);
    }

    /// <summary>
    /// Metodo encargado de calcular la posicion de la nave 
    /// </summary>
    /// <returns>Posicion x & y</returns>
    private Position CalculateTrilateration()
    {
        var a = Math.Pow(Kenobi.X, 2) + Math.Pow(Kenobi.Y, 2) - Math.Pow(Kenobi.R, 2);
        var b = Math.Pow(Skywalker.X, 2) + Math.Pow(Skywalker.Y, 2) - Math.Pow(Skywalker.R, 2);
        var c = Math.Pow(Sato.X, 2) + Math.Pow(Sato.Y, 2) - Math.Pow(Sato.R, 2);

        var x32 = Sato.X - Skywalker.X;
        var x13 = Kenobi.X - Sato.X;
        var x21 = Skywalker.X - Kenobi.X;

        var y32 = Sato.Y - Skywalker.Y;
        var y13 = Kenobi.Y - Sato.Y;
        var y21 = Skywalker.Y - Kenobi.Y;

        var x = (a * y32 + b * y13 + c * y21) / (2 * (Kenobi.X * y32 + Skywalker.X * y13 + Sato.X * y21));
        var y = (a * x32 + b * x13 + c * x21) / (2 * (Kenobi.Y * x32 + Skywalker.Y * x13 + Sato.Y * x21));

        var position = new Position
        {
            X = Math.Round(x),
            Y = Math.Round(y)
        };
        return position;
    }

}

