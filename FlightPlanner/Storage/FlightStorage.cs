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

        public List<Flight> GetFlightList()
        {
            return _flightStorage;
        }
    }
}
