using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        //private static List<Flight> _flightStorage = new ();
        private readonly FlightPlannerDbContext _context;
        //private static int _id = 0;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public void AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public void Clear()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public void DeleteFlight(int id)
        {
            _context.Flights.RemoveRange(_context.Flights.Where(f => f.Id == id));
            _context.SaveChanges();
        }

        public Flight? CheckForDuplicateFlight(Flight flight)
        {
            return _context.Flights.FirstOrDefault(f =>
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

        public bool CheckForWrongValuesInFlight(Flight flight)
        {
            return string.IsNullOrEmpty(flight.Carrier) || 
                   string.IsNullOrEmpty(flight.DepartureTime) || 
                   string.IsNullOrEmpty(flight.ArrivalTime) ||
                   string.IsNullOrEmpty(flight.From.City) ||
                   string.IsNullOrEmpty(flight.From.Country) || 
                   string.IsNullOrEmpty(flight.From.AirportCode) ||
                   string.IsNullOrEmpty(flight.To.City) ||
                   string.IsNullOrEmpty(flight.To.Country) ||
                   string.IsNullOrEmpty(flight.To.AirportCode);
        }

        public bool CheckForTheSameAirports(Flight flight)
        {
            return flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim();
        }

        public bool CheckForStrangeDate(Flight flight)
        {
            return flight.DepartureTime == flight.ArrivalTime ||
                   DateTime.Parse(flight.DepartureTime) > DateTime.Parse(flight.ArrivalTime);
        }

        public Flight? FindFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);
        }

        public Airport FindAirport(string airport)
        {
            var airportStr = airport.ToLower().Trim();

           return _context.Airports.First(f => 
                f.Country.ToLower().Substring(0, airportStr.Length) == airportStr || 
                f.City.ToLower().Substring(0, airportStr.Length) == airportStr ||
                f.AirportCode.ToLower().Substring(0, airportStr.Length) == airportStr);
        }

        public Flight[] FindFlights(SearchFlightsRequest request)
        {
            return _context.Flights
                .Where(f => f.From.AirportCode == request.From &&
                            f.To.AirportCode == request.To &&
                            f.DepartureTime.Substring(0, request.DepartureDate.Length) == request.DepartureDate)
                .ToArray();
        }

        public bool CheckForWrongValuesInRequest(SearchFlightsRequest request)
        {
            return string.IsNullOrEmpty(request.From) ||
                   string.IsNullOrEmpty(request.To) ||
                   string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
