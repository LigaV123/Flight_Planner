using FlightPlanner.Exceptions;
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
        private readonly FlightStorage _storage;

        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            return NotFound();
        }


        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            if (_storage.CheckForDuplicateFlight(flight) != null)
            {
                return Conflict();
            }

            if (_storage.CheckForWrongValues(flight) ||
                _storage.CheckForTheSameAirports(flight) ||
                _storage.CheckForStrangeDate(flight))
            {
                return BadRequest();
            }

            _storage.AddFlight(flight);

            return Created("", flight);
        }
    }
}
