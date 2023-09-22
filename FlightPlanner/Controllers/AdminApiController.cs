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
            var flightList = _storage.GetFlightList();
            
            if (flightList.Contains(flight))
            {
              return Conflict();
            }

            _storage.AddFlight(flight);
            return Created("", flight);
            
            //throw new DuplicateFlightException();
        }
    }
}
