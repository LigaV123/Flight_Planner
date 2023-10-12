using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validations
{
    public class FlightValuesValidation : IValidation
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier) &&
                   !string.IsNullOrEmpty(flight?.DepartureTime) &&
                   !string.IsNullOrEmpty(flight?.ArrivalTime) &&
                   flight?.From != null &&
                   flight?.To != null;
        }
    }
}
