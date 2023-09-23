using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightStorage _storage = new ();

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var flight = _storage.FindAirport(search);

            var data = new[]
            {
                new
                {
                    airport = flight.From.AirportCode,
                    city = flight.From.City,
                    country = flight.From.Country
                }
            };

            return Ok(JsonConvert.SerializeObject(data));
        }
    }
}
