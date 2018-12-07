using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.DLL;
using Tieto.Models;

namespace Tieto.BLL
{
    public class UserManager : IUserManager
    {
        public void Add(User user)
        {
            IUserDbProvider db = new UserDbProvider();

            db.Create(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetList()
        {
            throw new NotImplementedException();
        }

        public User Authenticate(string username, string password)
        {
            IUserDbProvider db = new UserDbProvider();

            return db.VerifyPassword(db.FindByUsername(username).ID, password);
        }

        public void GeneratePassword(User user, string password)
        {
            IUserDbProvider db = new UserDbProvider();

            db.CreatePassword(user, password);
        }
    }
}
