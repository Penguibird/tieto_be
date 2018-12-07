using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public interface IUserManager
    {

        IList<User> GetList();

        User Get(int id);

        void Add(User user);

        void Edit(int id);

        void Delete(int id);

        User Authenticate(string username, string password);

        void GeneratePassword(User user, string password);

    }
}
