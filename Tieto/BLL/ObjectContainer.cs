using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.BLL
{
    public static class ObjectContainer
    {

        public static ITripManager GetTripManager()
        {
            return new MockTripManager();
        }

        public static IUserManager GetUserManager()
        {
            return new UserManager();
        }

        /*public static ILocationManager GetLocationManager()
        {
            return new MockLocationManager();
        }*/

    }
}
