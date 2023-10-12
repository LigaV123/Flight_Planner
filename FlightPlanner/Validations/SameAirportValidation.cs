using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validations
{
    public class SameAirportValidation : IValidation
    {
        public bool IsValid(Flight flight)
        {
            return flight?.From?.AirportCode?.ToLower()?.Trim() != 
                   flight?.To?.AirportCode?.ToLower()?.Trim();
        }
    }
}
