using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }

        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(100)]
        public string City { get; set; }

        [JsonPropertyName("airport")]
        [StringLength(100)]
        public string AirportCode { get; set; }
    }
}