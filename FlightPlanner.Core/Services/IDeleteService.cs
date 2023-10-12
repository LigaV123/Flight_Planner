namespace FlightPlanner.Core.Services
{
    public interface IDeleteService : IDbService
    {
        void DeleteDbContext();
    }
}
