using FlightPlanner.Models;
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
            var airport = _storage.FindAirport(search);

            var data = new[]
            {
                new
                {
                    airport = airport.AirportCode,
                    city = airport.City,
                    country = airport.Country
                }
            };

            return Ok(JsonConvert.SerializeObject(data));
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightsRequest request)
        {
            if (_storage.CheckForWrongValuesInRequest(request) ||
                request.From == request.To)
            {
                return BadRequest();
            }

            var flightList = _storage.FindFlights(request);

            var data = new
                {
                    page = flightList.Length == 0 ? 0 : 1,
                    totalItems = flightList.Length,
                    items = flightList
                };

            return Ok(JsonConvert.SerializeObject(data));
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = _storage.FindFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
