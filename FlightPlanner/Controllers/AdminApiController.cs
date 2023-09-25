using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage = new ();
        private static readonly object _locker = new();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            if (_storage.FindFlightById(id) != null)
            {
                return Ok();
            }

            return NotFound();
        }


        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_locker)
            {
                if (_storage.CheckForDuplicateFlight(flight) >= 0)
                {
                    return Conflict();
                }

                if (_storage.CheckForWrongValuesInFlight(flight) ||
                _storage.CheckForTheSameAirports(flight) ||
                _storage.CheckForStrangeDate(flight))
                {
                    return BadRequest();
                }

                _storage.AddFlight(flight);
            }
            

            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                _storage.DeleteFlight(id);
            }

            return Ok();
        }
    }
}
