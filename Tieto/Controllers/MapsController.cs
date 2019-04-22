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
using Tieto.DLL;

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

        [HttpGet("validateCountry/{country}")]
        public async Task<Country[]> ValidateCountry(string country)
        {
            HttpClient c = new HttpClient();
            string key = System.IO.File.ReadAllText(Path.GetFullPath("~/../Assets/api_key.txt").Replace("~\\", ""));
            HttpResponseMessage m = await c.GetAsync("https://maps.googleapis.com/maps/api/place/autocomplete/json?language=en&key=" + key + "&types=(regions)&input=" + country);

            string response = await m.Content.ReadAsStringAsync();

            response = FixCountryNames(response);

            ICityDbProvider cityDbProvider = ObjectContainer.GetCityDbProvider();

            CityDT o = Newtonsoft.Json.JsonConvert.DeserializeObject<CityDT>(response);

            List<Country> countries = new List<Country>();

            for (var i = 0; i < o.Predictions.Length; i++)
            {
                var guess = cityDbProvider.GetCountryByName(o.Predictions[i].structured_formatting.main_text);
                if (guess != null && !countries.Contains(guess))
                {
                    countries.Add(guess);
                }
            }

            return countries.ToArray();
        }

        [HttpGet("validateCity/{city}")]
        public async Task<City[]> ValidateCity(string city)
        {
            HttpClient c = new HttpClient();
            string key = System.IO.File.ReadAllText(Path.GetFullPath("~/../Assets/api_key.txt").Replace("~\\", ""));
            HttpResponseMessage m = await c.GetAsync("https://maps.googleapis.com/maps/api/place/autocomplete/json?language=en&key=" + key + "&types=(cities)&input=" + city);

            string response = await m.Content.ReadAsStringAsync();

            response = FixCountryNames(response);

            ICityDbProvider cityDbProvider = ObjectContainer.GetCityDbProvider();

            CityDT o = Newtonsoft.Json.JsonConvert.DeserializeObject<CityDT>(response);

            string[] realCities = new string[o.Predictions.Length];
            string[] secs = new string[o.Predictions.Length];
            string[] realCountries = new string[o.Predictions.Length];
            City[] cities = new City[o.Predictions.Length];

            for (var i = 0; i < o.Predictions.Length; i++)
            {
                realCities[i] = o.Predictions[i].structured_formatting.main_text;
                secs[i] = o.Predictions[i].structured_formatting.secondary_text;
                realCountries[i] = (secs[i] == "" || secs[i] == null) ? realCities[i] : secs[i];
                if (realCountries[i].Contains(',')) realCountries[i] = realCountries[i].Split(',')[realCountries[i].Split(',').Length - 1];
                if (realCountries[i].ElementAt(0) == ' ') realCountries[i] = realCountries[i].Substring(1);
                cities[i] = new City
                {
                    Country = cityDbProvider.GetCountryByName(realCountries[i]),
                    GooglePlaceId = o.Predictions[i].place_id,
                    Name = realCities[i]
                };
            }
            return cities;
        }

        public static string FixCountryNames(string response)
        {

            //The names of some countries are slightly different in the database as compared to the values returned by GMaps API
            //This fixes it in one place, forever
            //If Google Maps ever changes a name of a country, this is the place to add an IF statemenet and thereby fix it

            if (response.Contains("Taiwan"))
            {
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
            if (response.Contains("Czechia"))
            {
                response = response.Replace("Czechia", "Czech Republic");
            }
            return response;
        }
    }
}
