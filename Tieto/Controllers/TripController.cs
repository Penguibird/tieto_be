﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.Models;

namespace Tieto.Controllers
{

    [Authorize]
    //Must be authorize
    [Route("api/[controller]")]
    public class TripController : Controller
    {

        private User GetUser()
        {
            return ObjectContainer.GetUserManager().Get(Convert.ToInt32(HttpContext.User.Identity.Name));
        }

        [HttpGet("getTripList")]
        public IList<Trip> GetTripList()
        {

            ITripManager m = ObjectContainer.GetTripManager();

            IList<Trip> trips = m.GetList(GetUser().ID);

            for (var i = 0; i < trips.Count; i++)
            {
                trips[i].ArrangePoints();
            }

            return trips;

        }

        [HttpPost("duplicate")]
        public Trip DuplicateTrip([FromBody] int id)
        {
            ITripManager m = ObjectContainer.GetTripManager();

            Trip orig = m.Get(id);

            Trip newTrip = ObjectContainer.Clone(orig);
            newTrip.Title = orig.Title != null && orig.Title != "" ? orig.Title + " - Copy" : "Copy of Unnamed trip";
            newTrip.ID = 0;
            newTrip.Exported = false;
            for (var i = 0; i < newTrip.Locations.Count(); i++)
            {
                var l = newTrip.Locations.ElementAt(i);
                newTrip.Locations.RemoveAt(i);
                newTrip.Locations.Insert(i, l);
                l.ID = 0;
                if (l.City != null)
                {
                    l.City.ID = 0;
                }
                if (l.Food != null)
                {
                    l.Food.ID = 0;
                }
            }
            if (newTrip.Exchange != null)
            {
                newTrip.Exchange.ID = 0;
                for (var i = 0; i < newTrip.Exchange.Rates.Count(); i++)
                {
                    newTrip.Exchange.Rates[i].ID = 0;
                }
            }
            
            m.Save(GetUser().ID, newTrip);

            newTrip.Locations.OrderBy(x => x.Position);

            return newTrip;
        }

        [HttpPost("delete")]
        public void DeleteTrip([FromBody] int id)
        {
            ITripManager m = ObjectContainer.GetTripManager();

            m.Delete(id);
        }

        [HttpGet("{id}")]
        public Trip TripDetails(int id)
        {
            ITripManager m = ObjectContainer.GetTripManager();

            Trip trip = m.Get(id);

            trip.Locations.OrderBy(x => x.ID);

            return trip;
        }

        private ClaimsIdentity GetIdentityFromToken(string token)
        {
            var tokenDecoder = new JwtSecurityTokenHandler();
            var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(token);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("iwjrgoirwhoinwriognmcgweiuohgowimeugmvetwiuhvgkjtejklgjwklfkwipockpoeqkgpovet")); //string is a sectret that should be replaced

            try
            {
                var principal = tokenDecoder.ValidateToken(
                    jwtSecurityToken.RawData,
                    new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = key,
                        ValidateIssuerSigningKey = false,
                        RequireExpirationTime = true,
                        RequireSignedTokens = false
                    },
                    out SecurityToken validatedToken
                );

                return principal.Identities.FirstOrDefault();
            }
            catch(SecurityTokenExpiredException e)
            {
                return null;
            }
        }

        [HttpPost("export/{id}")]
        public string Export(int id)
        {
            ITripManager m = ObjectContainer.GetTripManager();

            Trip t = m.Get(id);

            if (t.Title == null || t.Title == "" || t.Project == null || t.Project == "" || t.Purpose == null || t.Purpose == "" || t.Task == null || t.Task == "") return null;

            t.Exported = true;

            m.Save(GetUser().ID, t);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("iwjrgoirwhoinwriognmcgweiuohgowimeugmvetwiuhvgkjtejklgjwklfkwipockpoeqkgpovet")); //string is a sectret that should be replaced
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, t.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30), //Increase if problems with expiring tokens
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = "tieto-trippi-app",
                Audience = "everyone"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        [AllowAnonymous]
        [HttpGet("getPdf/{token}")]
        public IActionResult GetPdf(string token)
        {
            ITripManager m = ObjectContainer.GetTripManager();

            ClaimsIdentity identity = GetIdentityFromToken(token);

            if (identity == null)
            {
                return Unauthorized();
            }

            Trip t = m.Get(Convert.ToInt32(identity.Name));

            IPDFManager pm = ObjectContainer.GetPDFManager();

            //Don't let the user export it if Title or anything else is not defined!
            return File(pm.CreatePdf(t), "application/pdf"/*, t.Title + ".pdf"*/); //Uncomment this so that the file gets downloaded and not opened
        }

        [HttpPost("saveTrip")]
        public int SaveTrip([FromBody] Trip trip)
        {

            ITripManager m = ObjectContainer.GetTripManager();

            for (var i = 0; i < trip.Locations.Count; i++)
            {
                trip.Locations[i].Position = i;
            }

            return m.Save(GetUser().ID, trip);

        }

        [HttpPost("saveAndReturnTrip")]
        public Trip SaveAndReturnTrip([FromBody] Trip trip)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            IMapsManager mapsManager = ObjectContainer.GetMapsManager();

            for (var i = 0; i < trip.Locations.Count; i++)
            {
                trip.Locations[i].Position = i;
            }

            if (trip.ID > 0)
            {
                Trip old = m.Get(trip.ID);
                List<List<Location>> oldSections = mapsManager.SplitTripIntoSections(old);

                if (oldSections == null) return m.SaveAndReturn(GetUser().ID, trip);

                List<List<Location>> sections = mapsManager.SplitTripIntoSections(trip);

                //If a new point is added, it shows up as a new section here. If a transit point is added, it gets added to a section and changes the length
                Location modified = null;
                for (var i = 0; i < oldSections.Count; i++)
                {
                    if (oldSections[i].Count != sections[i].Count) modified = sections[i][1];
                }

                if (modified != null)
                {
                    mapsManager.SetSectionAsModified(GetUser(), trip, modified);
                }

            }

            return m.SaveAndReturn(GetUser().ID, trip);
        }

        [HttpPost("saveTitle/{id}")]
        public void SaveTitle(int id, [FromBody] string title)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            Trip t = m.Get(id);

            t.Title = title;

            m.Save(GetUser().ID, t);

        }

        [HttpPost("savePurpose/{id}")]
        public void SavePurpose(int id, [FromBody] string purpose)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            Trip t = m.Get(id);

            t.Purpose = purpose;

            m.Save(GetUser().ID, t);
        }

        [HttpPost("saveProject/{id}")]
        public void SaveProject(int id, [FromBody] string project)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            Trip t = m.Get(id);

            t.Project = project;

            m.Save(GetUser().ID, t);
        }

        [HttpPost("saveTask/{id}")]
        public void SaveTask(int id, [FromBody] string task)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            Trip t = m.Get(id);

            t.Task = task;

            m.Save(GetUser().ID, t);
        }

        [HttpPost("saveComment/{id}")]
        public void SaveComment(int id, [FromBody] string comment)
        {
            ITripManager m = ObjectContainer.GetTripManager();
            Trip t = m.Get(id);

            t.Comment = comment;

            m.Save(GetUser().ID, t);
        }

        /*[HttpPost("saveStartDate/{id}")]
        public ExchangeRate[] SaveStartDate(int id, [FromBody] DateTime dateTime)
        {
            var e = ObjectContainer.GetExchangeRateManager();
            var rates = e.FetchCurrencyResource(dateTime);

            ITripManager m = ObjectContainer.GetTripManager();

            Trip t = m.Get(id);
            t.StartDate = dateTime;
            t.ExchangeRates = rates;

            m.Save(GetUser().ID, t);

            return rates.ToArray();
        }*/

        [HttpPost("getExchangeRates")]
        public ExchangeRate[] GetExchangeRates([FromBody] long dateTime)
        {
            var e = ObjectContainer.GetExchangeRateManager();
            var rates = e.FetchCurrencyResource(dateTime);

            return rates.ToArray();
        }

        [HttpPost("editExchangeRate")]
        public void EditExchangeRate(int tripId, CurrencyCode currencyCode, [FromBody] double value)
        {

        }
    }
}
