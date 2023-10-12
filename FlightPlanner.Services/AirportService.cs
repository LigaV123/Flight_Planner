using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        { }

        public Airport SearchAirport(string phrase)
        {
            phrase = phrase.ToLower().Trim();

            return _context.Airports.First(a => a.AirportCode.ToLower().Contains(phrase) ||
                                                a.Country.ToLower().Contains(phrase) ||
                                                a.City.ToLower().Contains(phrase));
        }
    }
}
