﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public interface IUserDbProvider
    {

        void Create(User User);

        void Update(User User);

        User Read(int id);

        User FindByUsername(string username);

        void Delete(int id);

        void CreatePassword(User u, string Password);

        void AddTrip(int id, Trip trip);
            
        User VerifyPassword(int id, string Password);

    }
}
