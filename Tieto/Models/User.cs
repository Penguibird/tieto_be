using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class User
    {

        public int Id;
        public string Email;
        public string Password;
        public string FirstName;
        public string LastName;

        public IEnumerable<Trip> Trips;


    }
}
