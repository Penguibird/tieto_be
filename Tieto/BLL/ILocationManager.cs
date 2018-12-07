using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    interface ILocationManager
    {

        void Add();

        void Delete();

        void Edit();

        Location Get();

        Location GetList(Trip trip);

    }
}
