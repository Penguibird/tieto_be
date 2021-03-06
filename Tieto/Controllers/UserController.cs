﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.DT;
using Tieto.Models;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Tieto.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {

        [HttpGet("{test}")]
        public string Test(string test)
        {
            return test;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public UserDT Authenticate([FromBody] UserDT user)
        {
            IUserManager userManager = ObjectContainer.GetUserManager();

            User u = userManager.Authenticate(user.Username, user.Password);

            if (u == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("iwjrgoirwhoinwriognmcgweiuohgowimeugmvetwiuhvgkjtejklgjwklfkwipockpoeqkgpovet")); //string is a sectret that should be replaced
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, u.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(user.LongSign ? 365 : 1), //vary this based on the "stay signed in" toggle
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = "tieto-trippi-app",
                Audience = "everyone"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            if (u != null)
            {
                return new UserDT {
                    Username = u.Email,
                    Token = tokenString
                };
            }

            else
            {
                return null;
            }

            //authenticate using service

            //create token with userId
        
            //send user data
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public UserDT Register([FromBody] UserDT user)
        {
            IUserManager userManager = ObjectContainer.GetUserManager();

            User u = new User
            {
                Email = user.Username,
                FullName = user.FullName,
                SuperiorEmail = user.SuperiorEmail
            };

            userManager.GeneratePassword(u, user.Password);

            userManager.Add(u);

            return Authenticate(user);
        }

        [AllowAnonymous]
        [HttpPost("checkEmail")]
        public bool CheckEmail([FromBody] string email)
        {
            IUserManager u = ObjectContainer.GetUserManager();
            return u.GetByEmail(email) == null;
        }

        [HttpGet("tokenCheck")]
        public bool TokenCheck()
        {
            return true;
        }

    }

}
