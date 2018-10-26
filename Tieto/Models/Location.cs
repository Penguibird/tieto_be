using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Location
    {

        public int Id;
        public TravelType TravelType;
        public bool IsCrossing;
        public City City;
        public DateTime Arrival;
        public DateTime Departure;

    }

    public enum TravelType
    {
        CAR = 1,
        AEROPLANE = 2,
        TRAIN = 3
    }
}
