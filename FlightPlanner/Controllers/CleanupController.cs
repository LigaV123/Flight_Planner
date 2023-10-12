using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : ControllerBase
    {
        private readonly IDeleteService _dbDelete;

        public CleanupController(IDeleteService dbDelete)
        {
            _dbDelete = dbDelete;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _dbDelete.DeleteDbContext();

            return Ok();
        }
    }
}
