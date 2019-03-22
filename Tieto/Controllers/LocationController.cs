using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tieto.BLL;
using Tieto.Models;

namespace Tieto.Controllers
{
    [Authorize]
    //Must be authorize
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        
        [HttpPost("save")]
        public int SaveLocation(Location location)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            return m.Save(location);
        }

        [HttpPost("saveCity/{locationId}")]
        public void SaveCity(int locationId, [FromBody] string[] names) /*First name is city, second name is country, third is place_id*/
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            TrippiDb DbContext = new TrippiDb();

            l.City = new City
            {
                Name = names[0],
                CountryID = DbContext.Countries.First(c => c.Name == names[1]).ID,
                GooglePlaceId = names[2]
            };

            m.Save(l);
        }

        [HttpPost("saveInboundTravel/{locationId}")]
        public void SaveInboundTravel(int locationId, [FromBody] TravelType travelType)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.InboundTravelType = travelType;

            m.Save(l);
        }

        //ALL DateTime Time objects will seem to be in Utc, but in reality will just be whatever the user entered!

        [HttpPost("saveArrivalTime/{locationId}")]
        public void SaveArrivalTime(int locationId, [FromBody] long arrivalTime)
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
        }

        [HttpPost("saveArrivalDate/{locationId}")]
        public void SaveArrivalDate(int locationId, [FromBody] long arrivalDate)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.ArrivalDate = arrivalDate;

            m.Save(l);
        }

        [HttpPost("saveDepartureTime/{locationId}")]
        public void SaveDepartureTime(int locationId, [FromBody] long departureTime)
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
        }

        [HttpPost("saveDepartureDate/{locationId}")]
        public void SaveDepartureDate(int locationId, [FromBody] long departureDate)
        {
            ILocationManager m = ObjectContainer.GetLocationManager();
            Location l = m.Get(locationId);

            l.DepartureDate = departureDate;

            m.Save(l);
        }

        [HttpPost("getDateTime")]
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

    }
}