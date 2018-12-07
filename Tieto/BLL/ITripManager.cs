using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public interface ITripManager
    {

        IList<Trip> GetList(string username);

        Trip Get(int id);

        void Add(Trip trip);

        void Edit(int id);

        void Delete(int id);

        void Export(int id);
        
    }
}
