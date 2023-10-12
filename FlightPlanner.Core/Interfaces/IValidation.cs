using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Interfaces
{
    public interface IValidation
    {
        bool IsValid(Flight flight);
    }
}
