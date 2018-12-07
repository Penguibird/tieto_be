using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public class MockTripManager : ITripManager
    {
        public void Add(Trip trip)
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
    }
}
