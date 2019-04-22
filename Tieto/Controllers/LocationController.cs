using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tieto.BLL;
using Tieto.DLL;
using Tieto.Models;

namespace Tieto.Controllers
{
    [Authorize]
    //Must be authorize
    [Route("api/[controller]")]
    public class LocationController : Controller
    {

        private User GetUser()
        {
            return ObjectContainer.GetUserManager().Get(Convert.ToInt32(HttpContext.User.Identity.Name));
        }

        [HttpPost("save")]
        public int SaveLocation(Location location)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            return m.Save(location);
        }

        //CHECK FOR BORDER HERE
        [HttpPost("saveCity/{locationId}")]
        public async Task<Trip> SaveCity(int locationId, [FromBody] string[] names) /*First name is city, second name is country, third is place_id*/
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            ICityDbProvider c = ObjectContainer.GetCityDbProvider();

            if (names[0] == "" && names[1] == "" && names[2] == null)
            {
                l.City = null;
            }
            else
            {
                l.City = new City
                {
                    Name = names[0],
                    CountryID = c.GetCountryByName(names[1]).ID,
                    GooglePlaceId = names[2]
                };
            }

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        [HttpPost("saveCountry/{locationId}")]
        public Trip SaveCountry(int locationId, [FromBody] Country country)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();
            ITripManager tripManager = ObjectContainer.GetTripManager();
            Location l = m.Get(locationId);

            l.City.Country = country;
            l.SectionModified = true;
            var trip = tripManager.Get(l.TripId);

            mapsManager.SetSectionAsModified(GetUser(), trip, l);

            m.Save(l);

            return trip;
        }

        //CHECK FOR BORDER HERE
        [HttpPost("saveInboundTravel/{locationId}")]
        public async Task<Trip> SaveInboundTravel(int locationId, [FromBody] TravelType travelType)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.InboundTravelType = travelType;

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        //ALL DateTime Time objects will seem to be in Utc, but in reality will just be whatever the user entered!

        //CHECK FOR BORDER HERE            
        [HttpPost("saveArrivalTime/{locationId}")]
        public async Task<Trip> SaveArrivalTime(int locationId, [FromBody] long arrivalTime)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            if (arrivalTime == -1)
            {
                l.ArrivalTime = null;
            }
            else
            {
                l.ArrivalTime = arrivalTime;
            }

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        //CHECK FOR BORDER HERE
        [HttpPost("saveArrivalDate/{locationId}")]
        public async Task<Trip> SaveArrivalDate(int locationId, [FromBody] long arrivalDate)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.ArrivalDate = arrivalDate;

            if (l.ArrivalDate.HasValue && l.DepartureDate.HasValue)
            {
                if (l.Food == null)
                {
                    l.Food = CreateLocationFood((int)(l.DepartureDate / 24 / 60 / 60000 - l.ArrivalDate / 24 / 60 / 60000));
                }
                else
                {
                    l.Food = CreateLocationFood((int)(l.DepartureDate / 24 / 60 / 60000 - l.ArrivalDate / 24 / 60 / 60000), l.Food);
                }
            }

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        //CHECK FOR BORDER HERE
        [HttpPost("saveDepartureTime/{locationId}")]
        public async Task<Trip> SaveDepartureTime(int locationId, [FromBody] long departureTime)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            if (departureTime == -1)
            {
                l.DepartureTime = null;
            }
            else
            {
                l.DepartureTime = departureTime;
            }

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        //CHECK FOR BORDER HERE
        [HttpPost("saveDepartureDate/{locationId}")]
        public async Task<Trip> SaveDepartureDate(int locationId, [FromBody] long departureDate)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.DepartureDate = departureDate;

            if (l.ArrivalDate.HasValue && l.DepartureDate.HasValue && l.ArrivalDate.Value != -1 && l.DepartureDate != -1)
            {
                if (l.Food == null)
                {
                    l.Food = CreateLocationFood((int)(l.DepartureDate / 24 / 60 / 60000 - l.ArrivalDate / 24 / 60 / 60000));
                }
                else
                {
                    l.Food = CreateLocationFood((int)(l.DepartureDate / 24 / 60 / 60000 - l.ArrivalDate / 24 / 60 / 60000), l.Food);
                }
            }
            else
            {
                l.Food = null;
            }

            m.Save(l);

            IMapsManager map = ObjectContainer.GetMapsManager();
            ITripManager t = ObjectContainer.GetTripManager();
            Trip trip = t.Get(l.TripId);
            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await map.FillBorderPoints(GetUser(), tripX, l); }

            if (trip.Locations[0].ID == l.ID)
            {
                trip = t.SetExchangeRates(departureDate, trip);
            }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        [HttpPost("delete")]
        public Trip Delete([FromBody] int[] locationIds)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager tripManager = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location l = m.Get(locationIds[0]);
            Location lAlt = m.Get(locationIds[1]);

            Trip trip = tripManager.Get(l.TripId);

            //There will always be a crossing there and we want to see if the other one is a transit. If it is, the section, into which it belongs, is modified
            if (l.IsCrossing)
            {
                if (lAlt.Transit)
                {
                    mapsManager.SetSectionAsModified(GetUser(), trip, lAlt);
                }
            }
            else
            {
                if (l.Transit)
                {
                    mapsManager.SetSectionAsModified(GetUser(), trip, l);
                }
            }

            //Order in which they are deleted is important here, deleting from last to first in the array of locationIds
            for (var j = locationIds.Length - 1; j >= 0; j--)
            {
                for (var i = 0; i < trip.Locations.Count; i++)
                {
                    if (trip.Locations[i].ID == locationIds[j])
                    {
                        trip.Locations[i].Deleted = true;
                        break;
                    }
                }
            }

            tripManager.Save(GetUser().ID, trip);

            return trip;
        }

        [HttpPost("setBorderCrossTime/{locationId}")]
        public Trip SetBorderCrossTime(int locationId, [FromBody] long millis)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager t = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location l = m.Get(locationId);
            Trip trip = t.Get(l.TripId);

            l.CrossedAtTime = millis;
            l.SectionModified = true;

            if (trip.Locations[l.Position - 1].Transit)
            {
                trip.Locations[l.Position - 1].DepartureTime = millis;
            }
            if (trip.Locations[l.Position + 1].Transit)
            {
                trip.Locations[l.Position + 1].ArrivalTime = millis;
            }

            mapsManager.SetSectionAsModified(GetUser(), trip, l);

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);
            m.Save(l);

            return t.Get(l.TripId);

        }

        [HttpPost("setBorderCrossDate/{locationId}")]
        public Trip SetBorderCrossDate(int locationId, [FromBody] long millis)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager t = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location l = m.Get(locationId);
            Trip trip = t.Get(l.TripId);

            l.CrossedAtDate = millis;
            l.SectionModified = true;

            if (trip.Locations[l.Position - 1].Transit)
            {
                trip.Locations[l.Position - 1].DepartureDate = millis;
            }
            if (trip.Locations[l.Position + 1].Transit)
            {
                trip.Locations[l.Position + 1].ArrivalDate = millis;
            }

            mapsManager.SetSectionAsModified(GetUser(), trip, l);

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);
            m.Save(l);

            return t.Get(l.TripId);

        }

        [HttpPost("resetSectionModifications/{locationId}")]
        public async Task<Trip> ResetSectiondModifications(int locationId)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager t = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location l = m.Get(locationId);
            Trip trip = t.Get(l.TripId);

            l.SectionModified = false;

            mapsManager.SetSectionAsNotModified(GetUser(), trip, l);

            m.Save(l);

            using (Trip tripX = ObjectContainer.Clone(trip)) { trip = await mapsManager.FillBorderPoints(GetUser(), tripX, l); }

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            return trip;
        }

        //Possible optimization: Don't need to return the trip from the border cross seter, because no aditional user-facing stuff changes (but be careful about the Modified secions!)

        [HttpPost("setTransitCountryName/{locationId}")]
        public Trip SetTransitCountryName(int locationId, [FromBody] Country country)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager t = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location l = m.Get(locationId);
            Trip trip = t.Get(l.TripId);
            l.City.Country = country;
            l.City.CountryID = country.ID;
            l.SectionModified = true;

            mapsManager.SetSectionAsModified(GetUser(), trip, l);

            m.Save(l);

            return trip;
        }

        [HttpPost("addTransityCountry/{locationId}")]
        public void AddTransitCountry(int locationId)
        {
            //CHANGE THIS TO SAVE USING THE WHOLE TRIP SO THAT IT'S THE SAME AS CREATING NORMAL POINTS
            //This whole function might actually be useless
            //locationId is the id of the location BEHIND WHICH this new point will be added

            ILocationManager m = ObjectContainer.GetLocationManager();
            ITripManager t = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            Location orig = m.Get(locationId);
            int origPos = orig.Position;
            Trip trip = t.Get(orig.TripId);
            //First add the transit, then the crossing, because it pushes it forward and we're adding inbound travel.
            //Edit these points so that they are useful
            trip.Locations.Insert(origPos, new Location { Transit = true, City = new City { Name = "Transit_Country" } });
            trip.Locations.Insert(origPos, new Location { IsCrossing = true, CrossedBorder = true });

            trip.ArrangePoints();

            t.Save(GetUser().ID, trip);

            mapsManager.SetSectionAsModified(GetUser(), trip, trip.Locations[origPos]); //This will certianly be the new location as it replaced the original

            t.Save(GetUser().ID, trip);
        }

        public class SaveFoodObject
        {
            public int LocationId { get; set; }
            public int DayIndex { get; set; }
            public int FoodIndex { get; set; }
        }

        [HttpPost("saveFood")]
        public void SaveFood([FromBody] SaveFoodObject saveFoodObject)
        {

            ILocationManager l = ObjectContainer.GetLocationManager();

            LocationFood loc = l.Get(saveFoodObject.LocationId).Food;

            int foodIndex = saveFoodObject.FoodIndex;
            int dayIndex = saveFoodObject.DayIndex;

            if (dayIndex == 0 && loc.OnlyDay != null)
            {
                if (foodIndex == 0)
                {
                    loc.OnlyDay.Breakfast = !loc.OnlyDay.Breakfast;
                }
                else if (foodIndex == 1)
                {
                    loc.OnlyDay.Lunch = !loc.OnlyDay.Lunch;
                }
                else
                {
                    loc.OnlyDay.Dinner = !loc.OnlyDay.Dinner;
                }
            }
            else if (dayIndex == 0 && loc.FirstDay != null)
            {
                if (foodIndex == 0)
                {
                    loc.FirstDay.Breakfast = !loc.FirstDay.Breakfast;
                }
                else if (foodIndex == 1)
                {
                    loc.FirstDay.Lunch = !loc.FirstDay.Lunch;
                }
                else
                {
                    loc.FirstDay.Dinner = !loc.FirstDay.Dinner;
                }
            }
            else if (dayIndex == loc.MiddleDays.Count + 1)
            {
                if (foodIndex == 0)
                {
                    loc.LastDay.Breakfast = !loc.LastDay.Breakfast;
                }
                else if (foodIndex == 1)
                {
                    loc.LastDay.Lunch = !loc.LastDay.Lunch;
                }
                else
                {
                    loc.LastDay.Dinner = !loc.LastDay.Dinner;
                }
            }
            else
            {
                if (foodIndex == 0)
                {
                    loc.MiddleDays[dayIndex - 1].Breakfast = !loc.MiddleDays[dayIndex - 1].Breakfast;
                }
                else if (foodIndex == 1)
                {
                    loc.MiddleDays[dayIndex - 1].Lunch = !loc.MiddleDays[dayIndex - 1].Lunch;
                }
                else
                {
                    loc.MiddleDays[dayIndex - 1].Dinner = !loc.MiddleDays[dayIndex - 1].Dinner;
                }
            }

            Location location = l.Get(saveFoodObject.LocationId);
            location.Food = loc;
            l.Save(location);
        }

        private LocationFood CreateLocationFood(int days, LocationFood template)
        {

            LocationFood loc = new LocationFood();

            if (template.OnlyDay != null && days == 0)
            {
                //No change
                loc.OnlyDay = template.OnlyDay;
            }
            else if (template.OnlyDay != null && days > 0)
            {
                loc.FirstDay = template.OnlyDay;
                loc.LastDay = new DayFood(true, false, false);
                List<DayFood> l = new List<DayFood>();
                for (var i = 0; i < days - 2; i++)
                {
                    l.Add(new DayFood(true, false, false));
                }
                loc.MiddleDays = l;
            }
            else if (template.OnlyDay == null && days == 0)
            {
                //Take the first day ... or possibly change this?
                loc.OnlyDay = template.FirstDay;
            }
            else if (template.OnlyDay == null && days > 0)
            {
                //Take the first days if the new stuff is shorter ... or possibly change in accordance with the previous case?
                //If the new stuff is longer --> just add breakfasts
                loc.FirstDay = template.FirstDay;
                List<DayFood> l = new List<DayFood>();
                if (template.MiddleDays.Count() > days - 1)
                {
                    var i = 0;
                    for (; i < days - 2; i++)
                    {
                        l.Add(template.MiddleDays.ElementAt(i));
                    }
                    loc.LastDay = template.MiddleDays.ElementAt(i + 1);
                }
                else if (template.MiddleDays.Count() == days - 1)
                {
                    var i = 0;
                    for (; i < days - 1; i++)
                    {
                        l.Add(template.MiddleDays.ElementAt(i));
                    }
                    loc.LastDay = template.LastDay;
                }
                else
                {
                    var i = 0;
                    for (; i < template.MiddleDays.Count(); i++)
                    {
                        l.Add(template.MiddleDays.ElementAt(i));
                    }
                    for (var j = 0; j < days - 1 - i; j++)
                    {
                        l.Add(new DayFood(true, false, false));
                    }
                    loc.LastDay = template.LastDay;
                }
                loc.MiddleDays = l;
            }

            return loc;
        }
        private LocationFood CreateLocationFood(int days)
        {

            LocationFood loc = new LocationFood();

            if (days > 0)
            {
                loc.FirstDay = new DayFood(false, false, false);
                loc.LastDay = new DayFood(true, false, false);
                List<DayFood> l = new List<DayFood>();
                for (var i = 0; i < days - 1; i++)
                {
                    l.Add(new DayFood(true, false, false));
                }
                loc.MiddleDays = l;
            }
            else
            {
                loc.OnlyDay = new DayFood(false, false, false);
            }

            return loc;
        }

        [HttpPost("getDateTime")]
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

    }
}