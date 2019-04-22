using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class User
    {

        public int ID { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }
        public string SuperiorEmail { get; set; }

    }
}
