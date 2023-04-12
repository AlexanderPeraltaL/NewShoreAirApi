using Business;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace NewShoreAirApi.Controllers
{
    [ApiController]
    [Route("GetRoute")]
    public class GetRouteController : Controller
    {
        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Journey journey)
        {
            BusinessCheckAvaliableFlights businessCheckAvaliableFlights = new BusinessCheckAvaliableFlights(journey);
            Journey journeys = businessCheckAvaliableFlights.Process();
            if (journeys != null && journeys.Flights.Count > 0)
            {
                return Ok(journeys);
            }
            else
            {
                return NotFound("No se encontraron rutas para los destinos indicados");
            }
        }
    }
}
