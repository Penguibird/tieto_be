using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public interface ILocationManager
    {

        int Save(Location location);

        void Delete(int id);

        void Edit(int id);

        Location Get(int id);

        List<Location> GetList(Trip trip);

        List<Location> GetList(int tripId);

    }
}
