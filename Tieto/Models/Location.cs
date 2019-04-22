using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Location
    {

        public int ID { get; set; }
        public bool IsCrossing { get; set; }
        public bool Transit { get; set; }
        public long? CrossedAt { get; set; } //Number of milliseconds since departure from last point -- DEPRECATED, SHOULD BE REMOVED, NOT YET SAFE
        public long? CrossedAtDate { get; set; } //Absolute millisecond value
        public long? CrossedAtTime { get; set; } //Absolute millisecond value since midnight
        public long? DefaultCrossedAt { get; set; } //Not yet used and probably won't be used at all -- DEPRECATED
        public bool CrossedBorder { get; set; }
        public TravelType? InboundTravelType { get; set; }
        public City City { get; set; }
        //Milliseconds to midnight of the correct day
        public long? ArrivalDate { get; set; }
        //Milliseconds to correct time since midnight
        public long? ArrivalTime { get; set; }
        //Milliseconds to midnight of the correct day
        public long? DepartureDate { get; set; }
        //Milliseconds to correct time since midnight
        public long? DepartureTime { get; set; }
        public LocationFood Food { get; set; }
        public int TripId { get; set; }
        public int Position { get; set; }
        public bool Deleted { get; set; }
        public bool SectionModified { get; set; }
        //Look at the paper of which there is a photo to see what this is, it doesn't mean that this location was modified, it means the way in which borders are crossed
        //was modified in any of the locations of the GROUP, which are all the points between two points which are neither transit nor crossing
        //All locations in a section are marked as modified the moment even a single edit is made

    }

    public enum TravelType
    {
        CAR = 1,
        PUBLIC_TRANSPORT = 2,
        PLANE = 3,
        BOAT = 4
    }
}
