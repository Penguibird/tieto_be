using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public interface ILocationDbProvider
    {
        int Create(Location Location);

        int Update(Location Location);

        Location Read(int id);

        List<Location> FindByTripId(int tripId);

        void Delete(int id);
    }
}
