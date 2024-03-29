﻿using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models
{
    public class Flight : Entity
    {
        public Airport From { get; set; }
        public Airport To { get; set; }

        [StringLength(100)]
        public string Carrier { get; set; }
        [StringLength(100)]
        public string DepartureTime { get; set; }
        [StringLength(100)]
        public string ArrivalTime { get; set; }
    }
}
