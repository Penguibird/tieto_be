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
            IUserDbProvider db = ObjectContainer.GetUserDbProvider();

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
            IUserDbProvider db = ObjectContainer.GetUserDbProvider();

            return db.Read(id);
        }

        public IList<User> GetList()
        {
            throw new NotImplementedException();
        }

        public User Authenticate(string username, string password)
        {
            IUserDbProvider db = ObjectContainer.GetUserDbProvider();

            return db.VerifyPassword(db.FindByUsername(username).ID, password);
        }

        public void GeneratePassword(User user, string password)
        {
            IUserDbProvider db = ObjectContainer.GetUserDbProvider();

            db.CreatePassword(user, password);
        }

        public void AddTrip(int userId, Trip trip)
        {
            IUserDbProvider db = ObjectContainer.GetUserDbProvider();
          
            db.AddTrip(userId, trip);
        }

    }
}
