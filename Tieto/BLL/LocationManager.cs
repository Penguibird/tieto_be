using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.DLL;
using Tieto.Models;

namespace Tieto.BLL
{
    public class LocationManager : ILocationManager
    {
        public int Save(Location location)
        {
            ILocationDbProvider db = ObjectContainer.GetLocationDbProvider();

            if (location.ID == 0)
            {
                int id = db.Create(location);
                return id;
            }
            else
            {
                return db.Update(location);
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public Location Get(int id)
        {
            ILocationDbProvider db = ObjectContainer.GetLocationDbProvider();

            return db.Read(id);
        }

        public List<Location> GetList(Trip trip)
        {
            return trip.Locations.ToList();
        }

        public List<Location> GetList(int tripId)
        {
            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            return db.Read(tripId).Locations.ToList();
        }
    }
}
