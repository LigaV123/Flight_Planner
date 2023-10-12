using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using SearchFlightsRequest = FlightPlanner.Core.Models.SearchFlightsRequest;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public CustomerApiController(
            IAirportService airportService, 
            IMapper mapper,
            IFlightService flightService)
        {
            _airportService = airportService;
            _flightService = flightService;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var airport = _airportService.SearchAirport(search);

            var data = new[]
            {
                _mapper.Map<AirportRequest>(airport)
            };

            return Ok(data);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightsRequest request)
        {
            if (_flightService.CheckForWrongValuesInRequest(request) ||
                request.From == request.To)
            {
                return BadRequest();
            }

            var flightList = _flightService.FindFlights(request);

            var data = new
            {
                page = flightList.Length == 0 ? 0 : 1,
                totalItems = flightList.Length,
                items = flightList
            };

            return Ok(data);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightRequest>(flight));
        }
    }
}
