using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        bool CheckForDuplicateFlight(Flight flight);
        void DeleteFlightById(int id);

        bool CheckForWrongValuesInRequest(SearchFlightsRequest request);
        Flight[] FindFlights(SearchFlightsRequest request);
    }
}
