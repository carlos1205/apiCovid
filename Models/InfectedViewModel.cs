using System;

namespace Api.Models
{
    public class InfectedViewModel
    {
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}