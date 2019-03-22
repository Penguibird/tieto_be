using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.Models;

namespace Tieto.DLL
{
    public class LocationDbProvider : BaseDbProvider, ILocationDbProvider
    {
        public int Create(Location Location)
        {
            DbContext.Locations.Add(Location);
            DbContext.SaveChanges();
            return Location.ID;
        }

        public void Delete(int id)
        {
            DbContext.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public List<Location> FindByTripId(int tripId)
        {
            ITripDbProvider i = ObjectContainer.GetTripDbProvider();
            Trip t = i.Read(tripId);
            return t.Locations.ToList();
        }

        public Location Read(int id)
        {
            Location loc = DbContext.Locations.
                Include("City").
                Include("CrossingFrom").
                Include("CrossingTo").
                Include("Food").
            First(l => l.ID == id);

            loc.City.Country = DbContext.Countries.Find(loc.City.CountryID);
            return loc;

        }

        public int Update(Location location)
        {
            DbContext.Update(location);
            DbContext.SaveChanges();
            return location.ID;
        }
    }
}
