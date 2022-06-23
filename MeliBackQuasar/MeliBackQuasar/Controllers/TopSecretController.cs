using Microsoft.AspNetCore.Mvc;

namespace MeliBackQuasar.Controllers;

[ApiController]
[Route("[controller]")]
public class TopSecretController : ControllerBase
{
    private readonly ILocationService locationService;

    public TopSecretController(ILocationService locationService)
    {
        this.locationService = locationService;
    }

    /// <summary>
    /// Controlador encargado de obtener la posicion de la nave
    /// </summary>
    /// <param name="satelliteRequest">Modelo de satelites</param>
    /// <returns>Retorna Posicion (x,y) y mensaje</returns>
    [HttpPost]
    public IActionResult Post(SatelliteRequest satelliteRequest)
    {
        try
        {
            var response = locationService.GetLocation(satelliteRequest);
            return Ok(response);
        }
        catch (GeneralException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    /// <summary>
    /// Controlador encargado de obtener la posicion de la nave desde base de datos
    /// </summary>
    /// <returns>Retorna Posicion (x,y) y mensaje</returns>
    [HttpGet("/topsecret_split")]
    public IActionResult TopSecretSplit()
    {
        try
        {
            var response = locationService.GetLocation();
            return Ok(response);
        }
        catch (GeneralException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    /// <summary>
    /// Controlador se Encarga de guardar la informacion enviada de la nave
    /// </summary>
    /// <param name="name">Nombre de los Satelites: Kenobi, Skywalker, Sato</param>
    /// <param name="satellite">Los parametros de los satelites</param>
    /// <returns>True si se almaceno correctamente</returns>
    [HttpPost("/topsecret_split/{name}")]
    public IActionResult TopSecretSplit(string name, SatelliteSplit satellite)
    {
        try
        {
            var response = locationService.CreateLocation(name, satellite);
            return Ok(response);
        }
        catch (GeneralException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}

