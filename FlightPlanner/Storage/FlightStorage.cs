using System.Reflection;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flight> _flightStorage = new ();
        private static int _id = 0;

        public void AddFlight(Flight flight)
        {
            flight.Id = _id++;
            _flightStorage.Add(flight);
        }

        public void Clear()
        {
            _flightStorage.Clear();
        }

        public Flight? CheckForDuplicateFlight(Flight flight)
        {
            return _flightStorage.Where(f =>
                f.From.Country == flight.From.Country &&
                f.From.City == flight.From.City &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.Country == flight.To.Country &&
                f.To.City == flight.To.City &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime)
                .ToList().FirstOrDefault();
        }

        public bool CheckForWrongValues(Flight flight)
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
            return flight.From.Country.ToLower() == flight.To.Country.ToLower() ||
                   flight.From.City.ToLower() == flight.To.City.ToLower() ||
                   flight.From.AirportCode.ToLower() == flight.To.AirportCode.ToLower();
        }

        public bool CheckForStrangeDate(Flight flight)
        {
            return flight.DepartureTime == flight.ArrivalTime ||
                   DateTime.Parse(flight.DepartureTime) > DateTime.Parse(flight.ArrivalTime);
        }
    }
}
