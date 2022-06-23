using Infraestructure.Models;

namespace Infraestructure.Repositories;

public interface ILocationRepository
{
    /// <summary>
    /// Repositorio encargado de crear en base de datos las ubicaciones
    /// </summary>
    /// <param name="location">Modelo de ubicacion recibido en topsecret_split</param>
    /// <returns>Retorna True si se creo correctamente</returns>
    bool Create(LocationModel location);

    /// <summary>
    /// Repositorio encargado de retornar las ubicaciones
    /// </summary>
    /// <returns>Lista de Ubicaciones</returns>
    List<LocationModel> GetLocation();

    /// <summary>
    /// Repositorio encargado de crear o actualizar ubicacion
    /// </summary>
    /// <param name="location">Modelo de ubicacion recibido en topsecret_split</param>
    /// <returns></returns>
    bool CreateOrUpdate(LocationModel location);
}

