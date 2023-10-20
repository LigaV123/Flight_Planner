using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class DeleteService : DbService, IDeleteService
    {
        public DeleteService(IFlightPlannerDbContext context) : base(context)
        { }

        public void DeleteDbContext()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
        }
    }
}
