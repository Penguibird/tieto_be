﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public interface ICityDbProvider
    {
        Country GetCountryByName(string name);
    }
}
