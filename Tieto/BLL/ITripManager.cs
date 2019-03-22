using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public interface ITripManager
    {

        IList<Trip> GetList(int userId);

        Trip Get(int id);

        int Save(int userId, Trip trip);

        Trip SaveAndReturn(int userId, Trip trip);

        void Edit(int id);

        void Delete(int id);

        void Export(int id);
        
    }
}
