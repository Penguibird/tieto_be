using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tieto.BLL;
using Tieto.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Tieto.DT;

namespace Tieto.Controllers
{
    //[Authorize]
    //Must be authorize
    [Route("api/[controller]")]
    public class MapsController : Controller
    {
        /*private User GetUser()
        {
            return ObjectContainer.GetUserManager().Get(Convert.ToInt32(HttpContext.User.Identity.Name));
        }*/

        [HttpGet("validateCity/{city}")]
        public async Task<object> ValidateCity(string city)
        {
            HttpClient c = new HttpClient();
            string key = System.IO.File.ReadAllText(Path.GetFullPath("~/../Assets/api_key.txt").Replace("~\\", ""));
            HttpResponseMessage m = await c.GetAsync("https://maps.googleapis.com/maps/api/place/autocomplete/json?language=en&key=" + key + "&types=(cities)&input=" + city);

            string response = await m.Content.ReadAsStringAsync();

            //The names of some countries are slightly different in the database as compared to the values returned by GMaps API
            //This fixes it in one place, forever
            //If Google Maps ever changes a name of a country, this is the place to add an IF statemenet and thereby fix it
            if (response.Contains("Taiwan")) {
                response = response.Replace("Taiwan", "Tchaj-wan");
            }
            if (response.Contains("Bermuda"))
            {
                response = response.Replace("Bermuda", "Bermudas");
            }
            if (response.Contains("Jamaica") ||
                response.Contains("Cayman Islands") ||
                response.Contains("Turks and Caicos Islands") ||
                response.Contains("Haiti") ||
                response.Contains("Dominican Republic") ||
                response.Contains("Puerto Rico") ||
                response.Contains("British Virgin Islands") ||
                response.Contains("US Virgin Islands") ||
                response.Contains("Anguilla") ||
                response.Contains("Antigua and Barbuda") ||
                response.Contains("Guadeloupe") ||
                response.Contains("Montserrat") ||
                response.Contains("Dominica") ||
                response.Contains("Martinique") ||
                response.Contains("Barbados") ||
                response.Contains("St Vincent and the Grenadines") ||
                response.Contains("Grenada") ||
                response.Contains("Trinidad and Tobago") ||
                response.Contains("St Lucia"))
            {
                response = response.Replace("Jamaica", "Caribbean"); response = response.Replace("Cayman Islands", "Caribbean"); response = response.Replace("Turks and Caicos Islands", "Caribbean"); response = response.Replace("Haiti", "Caribbean");
                response = response.Replace("Dominican Republic", "Caribbean"); response = response.Replace("Puerto Rico", "Caribbean"); response = response.Replace("British Virgin Islands", "Caribbean"); response = response.Replace("US Virgin Islands", "Caribbean");
                response = response.Replace("Anguilla", "Caribbean"); response = response.Replace("Antigua and Barbuda", "Caribbean"); response = response.Replace("Guadeloupe", "Caribbean"); response = response.Replace("Montserrat", "Caribbean");
                response = response.Replace("Dominica", "Caribbean"); response = response.Replace("Martinique", "Caribbean"); response = response.Replace("Barbados", "Caribbean"); response = response.Replace("St Vincent and the Grenadines", "Caribbean");
                response = response.Replace("Grenada", "Caribbean"); response = response.Replace("Trinidad and Tobago", "Caribbean"); response = response.Replace("St Lucia", "Caribbean");
            }
            if (response.Contains("North Korea"))
            {
                response = response.Replace("North Korea", "Democratic People's Republic of Korea");
            }
            if (response.Contains("Comoros"))
            {
                response = response.Replace("Comoros", "Federal Islamic Republic of the Comoros");
            }
            if (response.Contains("Côte d'Ivoire"))
            {
                response = response.Replace("Côte d'Ivoire", "Ivory Cost");
            }
            if (response.Contains("Kyrgyzstan"))
            {
                response = response.Replace("Kyrgyzstan", "Kyrgyz Republic");
            }
            if (response.Contains("South Korea"))
            {
                response = response.Replace("South Korea", "Republic of Korea");
            }
            if (response.Contains("Serbia"))
            {
                response = response.Replace("Serbia", "Republic of Serbia");
            }
            if (response.Contains("São Tomé and Príncipe"))
            {
                response = response.Replace("São Tomé and Príncipe", "Sao Tome and Principe");
            }
            if (response.Contains("Myanmar (Burma)"))
            {
                response = response.Replace("Myanmar (Burma)", "Union of Myanmar");
            }
            if (response.Contains("UK"))
            {
                response = response.Replace("UK", "United Kingdom");
            }
            if (response.Contains("USA"))
            {
                response = response.Replace("USA", "United States Of America");
            }

            TrippiDb DbContext = new TrippiDb();

            CityDT o = Newtonsoft.Json.JsonConvert.DeserializeObject<CityDT>(response);
            string realCity = o.Predictions[0].structured_formatting.main_text;
            var sec = o.Predictions[0].structured_formatting.secondary_text;
            string realCountry = (sec == "" || sec == null) ? realCity : sec;
            realCountry = realCountry.Split(", ")[realCountry.Split(", ").Length - 1];
            return new City
            {
                Country = DbContext.Countries.First(country => country.Name == realCountry),
                GooglePlaceId = o.Predictions[0].place_id,
                Name = realCity
            };
        }
    }
}
