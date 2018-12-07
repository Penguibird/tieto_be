using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public class UserDbProvider : BaseDbProvider, IUserDbProvider
    {
        public void Create(User User)
        {
            DbContext.Add(User);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var u = Read(id);

            DbContext.Remove(u);

            DbContext.SaveChanges();
        }

        public User FindByUsername(string username)
        {
            IList<User> l = DbContext.Users.Where(u => u.Email == username).ToList();

            if (l.Count > 1)
            {
                //Log problem! More than one user with a username
                throw new Exception("Mrre than 1 ussr w/ d nejm m8");
            }
            else if (l.Count == 0)
            {
                return null;
            }
            else
            {
                return l.ElementAt(0);
            }
        }

        public User Read(int id)
        {
            return DbContext.Users.Find(id);
        }

        public void Update(User User)
        {
            throw new NotImplementedException();
        }

        public void CreatePassword(User u, string Password)
        {
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Woho m8, be careful what you wish for w/ yo passwrrd");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;

                DbContext.SaveChanges();
            }
        }

        public User VerifyPassword(int id, string Password)
        {
            var storedSalt = Read(id).PasswordSalt;
            var storedHash = Read(id).PasswordHash;

            if (Password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return null;
                }
            }

            return Read(id);
        }
    }
}
