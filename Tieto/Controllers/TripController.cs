using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.Models;

namespace Tieto.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class TripController : Controller
    {

        [HttpGet("{username}")]
        public IList<Trip> GetTripList(string username)
        {

            ITripManager m = ObjectContainer.GetTripManager();



            return m.GetList(username);

        }

    }
}
