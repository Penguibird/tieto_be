using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.BLL
{
    public interface IPDFManager
    {
        byte[] CreatePdf(Trip trip);
    }
}
