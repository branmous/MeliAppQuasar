namespace Domain.Services.Location;

public interface ILocationService
{
    /// <summary>
    /// Metodo encargado de obtener la ubicacion y el mensaje de la nave emisor
    /// </summary>
    /// <param name="satelliteRequest">Modelo de TopSecret</param>
    /// <returns>retorna posicion (x,y) y mensaje</returns>
    PositionSatelliteResponse GetLocation(SatelliteRequest satelliteRequest);
    /// <summary>
    /// Metodo encargado de la ubicacion y el mensaje de la nave emisor
    /// guargada en base de datos
    /// </summary>
    /// <returns>retorna posicion (x,y) y mensaje</returns>
    PositionSatelliteResponse GetLocation();

    /// <summary>
    /// Metodo encargado de guardar los datos de topSecret_split
    /// </summary>
    /// <param name="name">Nombre del Satelite</param>
    /// <param name="satellite">Modelo de distancia y mensaje del satelite</param>
    /// <returns>Retorna true si el mensaje fue guardado correctamente</returns>
    bool CreateLocation(string name, SatelliteSplit satellite);
}

