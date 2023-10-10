using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CleanupController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _dbService.DeleteRange<Flight>();
            _dbService.DeleteRange<Airport>();

            return Ok();
        }
    }
}
