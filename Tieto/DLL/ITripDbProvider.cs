using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public interface ITripDbProvider
    {

        int Create(Trip Trip, int userId);

        int Update(Trip Trip, int userId);

        Trip CreateAndReturn(Trip Trip, int userId);

        Trip UpdateAndReturn(Trip Trip, int userId);

        Trip Read(int id);

        List<Trip> FindByUserId(int userId);

        void Delete(int id);

    }
}
