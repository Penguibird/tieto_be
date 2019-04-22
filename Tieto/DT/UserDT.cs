using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.DT
{
    public class UserDT
    {

        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LongSign { get; set; }
        public string FullName { get; set; }
        public string SuperiorEmail { get; set; }
        public string Token { get; set; }

    }
}
