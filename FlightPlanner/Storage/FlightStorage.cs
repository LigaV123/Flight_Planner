using System.Reflection;
using FlightPlanner.Exceptions;
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

        public void DeleteFlight(int id)
        {
            _flightStorage.RemoveAll(f => f.Id == id);
        }

        public int CheckForDuplicateFlight(Flight flight)
        {
            if (_flightStorage.Count == 0) 
            {
                return -1;
            }

            return _flightStorage.FindIndex(f =>
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
            return _flightStorage.FirstOrDefault(f => f.Id == id);
        }

        public Flight FindAirport(string airport)
        {
            var airportStr = airport.ToLower().Trim();

            var flight = _flightStorage.First(f => 
                f.From.Country.ToLower().Substring(0, airportStr.Length) == airportStr || 
                f.From.City.ToLower().Substring(0, airportStr.Length) == airportStr ||
                f.From.AirportCode.ToLower().Substring(0, airportStr.Length) == airportStr);

            return flight;
        }

        public Flight[] FindFlights(SearchFlightsRequest request)
        {
            return _flightStorage
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
