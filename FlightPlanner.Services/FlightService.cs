using Azure.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        { }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights.Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool CheckForDuplicateFlight(Flight flight)
        {
            return _context.Flights.Any(f =>
                f.From.Country == flight.From.Country &&
                f.From.City == flight.From.City &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.Country == flight.To.Country &&
                f.To.City == flight.To.City &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime);
        }

        public void DeleteFlightById(int id)
        {
            _context.Flights.RemoveRange(_context.Flights.Where(f => f.Id == id));
            _context.SaveChanges();
        }

        public bool CheckForWrongValuesInRequest(SearchFlightsRequest request)
        {
            return string.IsNullOrEmpty(request.From) ||
                   string.IsNullOrEmpty(request.To) ||
                   string.IsNullOrEmpty(request.DepartureDate);
        }

        public Flight[] FindFlights(SearchFlightsRequest request)
        {
            return _context.Flights.Where(f => 
                f.From.AirportCode == request.From &&
                f.To.AirportCode == request.To &&
                f.DepartureTime.Contains(request.DepartureDate)).ToArray();
        }
    }
}
