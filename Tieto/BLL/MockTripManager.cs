using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public class MockTripManager : ITripManager
    {
        public int Save(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
            return new Trip
            {
                ID = id,
                Locations = null
            };
        }

        public IList<Trip> GetList(string username)
        {
            List<Trip> l = new List<Trip>();

            l.Add(this.Get(1));

            return l;
        }

        public int Save(int userId, Trip trip)
        {
            throw new NotImplementedException();
        }

        public IList<Trip> GetList(int userId)
        {
            throw new NotImplementedException();
        }

        public Trip SaveAndReturn(int userId, Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
