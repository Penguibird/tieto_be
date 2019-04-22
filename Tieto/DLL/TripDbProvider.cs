using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.Models;

namespace Tieto.DLL
{ 

    public class TripDbProvider : BaseDbProvider, ITripDbProvider
    {

        public int Create(Trip trip, int userId)
        {
            DbContext.Trips.Add(trip);
            trip.UserID = userId;
            DbContext.SaveChanges();
            return trip.ID;
        }
        public Trip CreateAndReturn(Trip trip, int userId)
        {
            DbContext.Trips.Add(trip);
            trip.UserID = userId;
            DbContext.SaveChanges();
            return trip;
        }

        public void Delete(int id)
        {
            Trip t = Read(id);
            t.Deleted = true;
            DbContext.Update(t);
            DbContext.SaveChanges();
        }

        public List<Trip> FindByUserId(int userId)
        {
            List<Trip> trips = DbContext.Trips.
                Include("Locations.City").
                Include("Locations.Food.FirstDay").
                Include("Locations.Food.MiddleDays").
                Include("Locations.Food.LastDay").
                Include("Locations.Food.OnlyDay").
                Include("Exchange.Rates").
                Where(t => t.UserID == userId && !t.Deleted).ToList();

            for (var j = 0; j < trips.Count; j++)
            {
                for (var i = 0; i < trips[j].Locations.Count; i++)
                {
                    if (trips[j].Locations[i].City == null)
                    {
                        continue;
                    }
                    else if (trips[j].Locations[i].City.CountryID == 0)
                    {
                        trips[j].Locations[i].City.Country = new Country { Name = "" };
                    }
                    else
                    {
                        trips[j].Locations[i].City.Country = DbContext.Countries.Find(trips[j].Locations[i].City.CountryID);
                    }
                }
            }

            return trips;
        }

        public Trip Read(int id)
        {
            Trip trip = DbContext.Trips.
                Include(t => t.Locations).ThenInclude(l => l.City).
                Include(t => t.Locations).ThenInclude(l => l.Food).ThenInclude(l => l.FirstDay).
                Include(t => t.Locations).ThenInclude(l => l.Food).ThenInclude(l => l.MiddleDays).
                Include(t => t.Locations).ThenInclude(l => l.Food).ThenInclude(l => l.LastDay).
                Include(t => t.Locations).ThenInclude(l => l.Food).ThenInclude(l => l.OnlyDay).
                Include("Exchange.Rates").
            FirstOrDefault(t => t.ID == id);
            
            for (var i = 0; i < trip.Locations.Count; i++)
            {
                if (trip.Locations[i].City == null)
                {
                    continue;
                }
                else if (trip.Locations[i].City.CountryID == 0)
                {
                    trip.Locations[i].City.Country = new Country { Name = "" };
                }
                else
                {
                    trip.Locations[i].City.Country = DbContext.Countries.Find(trip.Locations[i].City.CountryID);
                }
            }

            return trip;
        }

        public int Update(Trip trip, int userId)
        {
            DbContext.Update(trip);
            trip.UserID = userId;
            //ILocationDbProvider locationDbProvider = ObjectContainer.GetLocationDbProvider();
            for (var i = 0; i < trip.Locations.Count; i++)
            {
                if (trip.Locations[i].Deleted)
                {
                    DbContext.Entry(trip.Locations[i]).State = EntityState.Deleted;
                    //locationDbProvider.Delete(trip.Locations[i].ID);
                    //Dangerous code that will break things
                }
            }
            if (trip.Exchange != null && trip.Exchange.Deleted) DbContext.Entry(trip.Exchange).State = EntityState.Deleted;
            DbContext.SaveChanges();
            return trip.ID;
        }

        public Trip UpdateAndReturn(Trip trip, int userId)
        {
            DbContext.Update(trip);
            trip.UserID = userId;
            //ILocationDbProvider locationDbProvider = ObjectContainer.GetLocationDbProvider();
            for (var i = 0; i < trip.Locations.Count; i++)
            {
                if (trip.Locations[i].Deleted)
                {
                    DbContext.Entry(trip.Locations[i]).State = EntityState.Deleted;
                    //locationDbProvider.Delete(trip.Locations[i].ID);
                    //Dangerous code that will break things
                }
            }
            if (trip.Exchange != null && trip.Exchange.Deleted) DbContext.Entry(trip.Exchange).State = EntityState.Deleted;
            DbContext.SaveChanges();
            return trip;
        }
    }
}
