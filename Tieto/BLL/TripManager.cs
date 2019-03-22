using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.DLL;
using Tieto.Models;

namespace Tieto.BLL
{
    public class TripManager : ITripManager
    {
        public int Save(int userId, Trip trip)
        {

            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            if (trip.ID == 0)
            {
                return db.Create(trip, userId);
            }
            else
            {
                return db.Update(trip, userId);
            }

        }

        public Trip SaveAndReturn(int userId, Trip trip)
        {
            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            if (trip.ID == 0)
            {
                return db.CreateAndReturn(trip, userId);
            }
            else
            {
                return db.UpdateAndReturn(trip, userId);
            }
        }

        public void Delete(int id)
        {
            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            db.Delete(id);
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public void Export(int id)
        {
            throw new NotImplementedException();
        }

        public Trip Get(int id)
        {
            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            return db.Read(id);
        }

        public IList<Trip> GetList(int userId)
        {
            ITripDbProvider db = ObjectContainer.GetTripDbProvider();

            return db.FindByUserId(userId);
        }

    }
}
