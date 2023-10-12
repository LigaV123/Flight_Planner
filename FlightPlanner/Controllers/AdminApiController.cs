using AutoMapper;
using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidation> _validators;
        private static readonly object _locker = new();

        public AdminApiController(
            IFlightService flightService, 
            IMapper mapper,
            IEnumerable<IValidation> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight != null)
            {
                return Ok(_mapper.Map<FlightRequest>(flight));
            }

            return NotFound();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);
            lock (_locker)
            {
                if (!_validators.All(v => v.IsValid(flight)))
                {
                    return BadRequest();
                }

                if (_flightService.CheckForDuplicateFlight(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);
            }

            request = _mapper.Map<FlightRequest>(flight);

            return Created("", request);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                _flightService.DeleteFlightById(id);
            }
            
            return Ok();
        }
    }
}
