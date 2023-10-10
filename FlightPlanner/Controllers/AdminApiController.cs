using Azure.Core;
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
        private readonly IEntityService<Flight> _flightService;
        private static readonly object _locker = new();

        public AdminApiController(IEntityService<Flight> flightService)
        {
            _flightService = flightService;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            if (_flightService.GetById(id) != null)
            {
                return Ok();
            }

            return NotFound();
        }


        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {
            var flight = MapToFlight(request);
            lock (_locker)
            {
                //if (_flightService.CheckForDuplicateFlight(flight) != null)
                //{
                //    return Conflict();
                //}

                //if (_flightService.CheckForWrongValuesInFlight(flight) ||
                //_flightService.CheckForTheSameAirports(flight) ||
                //_flightService.CheckForStrangeDate(flight))
                //{
                //    return BadRequest();
                //}

                _flightService.Create(flight);
            }

            request = MapToFlightRequest(flight);

            return Created("", request);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                var flight = _flightService.GetById(id);
                _flightService.Delete(flight);
            }

            return Ok();
        }

        private Flight MapToFlight(FlightRequest request)
        {
            return new Flight
            {
                Id = request.Id,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                From = new Airport
                {
                    Country = request.From.Country,
                    City = request.From.City,
                    AirportCode = request.From.Airport
                },
                To = new Airport
                {
                    Country = request.To.Country,
                    City = request.To.City,
                    AirportCode = request.To.Airport
                }
            };
        }

        private FlightRequest MapToFlightRequest(Flight flight)
        {
            return new FlightRequest
            {
                Id = flight.Id,
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                From = new AirportRequest
                {
                    Country = flight.From.Country,
                    City = flight.From.City,
                    Airport = flight.From.AirportCode
                },
                To = new AirportRequest
                {
                    Country = flight.To.Country,
                    City = flight.To.City,
                    Airport = flight.To.AirportCode
                }
            };
        }
    }
}
