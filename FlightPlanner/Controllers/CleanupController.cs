using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : ControllerBase
    {
        private readonly FlightStorage _storage;

        public CleanupController()
        {
            _storage = new FlightStorage();
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _storage.Clear();

            return Ok();
        }
    }
}
