using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Location
    {

        public int ID { get; set; }
        public TravelType TravelType { get; set; }
        public bool IsCrossing { get; set; }
        public City City { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Trip Trip { get; set; }

    }

    public enum TravelType
    {
        CAR = 1,
        AEROPLANE = 2,
        TRAIN = 3
    }
}
