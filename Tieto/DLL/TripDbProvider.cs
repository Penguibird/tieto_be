using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{ 

    public class TripDbProvider : BaseDbProvider, ITripDbProvider
    {

        public void Create(Trip Trip)
        {
            DbContext.Trips.Add(Trip);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DbContext.Trips.Remove(Read(id));
            DbContext.SaveChanges();
        }

        public Trip Read(int id)
        {
            return DbContext.Trips.Find(id);
        }

        public void Update(Trip Trip)
        {
            throw new NotImplementedException();
        }
    }
}
