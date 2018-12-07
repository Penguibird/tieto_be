using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    interface ITripDbProvider
    {

        void Create(Trip Trip);

        void Update(Trip Trip);

        Trip Read(int id);

        void Delete(int id);

    }
}
