using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tieto.Controllers;
using Tieto.DLL;
using Tieto.Models;

namespace Tieto.BLL
{
    public class MapsManager : IMapsManager
    {

        //The border points are unnecesairly filled even when not all the stuff is filled in

        public List<List<Location>> SplitTripIntoSections(Trip trip)
        {
            if (trip.Locations.Count < 3) return null;
            List<Location> locations = trip.Locations.ToList();
            List<int[]> sectionOutlines = new List<int[]>();
            for (var i = 0; i < locations.Count; i++)
            {
                if (!locations[i].Transit && !locations[i].IsCrossing)
                {
                    if (sectionOutlines.Count != 0)
                    {
                        sectionOutlines[sectionOutlines.Count - 1][1] = i;
                    }
                    sectionOutlines.Add(new int[] { i, -1 });
                }
            }
            sectionOutlines.RemoveAt(sectionOutlines.Count - 1);

            List<List<Location>> sections = new List<List<Location>>();

            for (var i = 0; i < sectionOutlines.Count; i++)
            {
                int start = sectionOutlines[i][0];
                int finish = sectionOutlines[i][1];
                List<Location> locs = new List<Location>();
                for (int j = start; j <= finish; j++)
                {
                    locs.Add(locations[j]);
                }
                sections.Add(locs);
            }

            return sections;
        }

        public List<Location> GetSectionFromLocation(Trip trip, Location location)
        {
            List<List<Location>> sections = SplitTripIntoSections(trip);
            var i = 0;
            for (; i < sections.Count; i++)
            {
                if (sections[i].Contains(location)) break;
            }
            return sections[i];
        }

        public void SetSectionAsModified(User user, Trip trip, Location location)
        {
            List<List<Location>> sections = SplitTripIntoSections(trip);
            for (var i = 0; i < sections.Count; i++)
            {
                var contains = false;
                for (var k = 0; k < sections[i].Count; k++)
                {
                    if (sections[i][k].ID == location.ID) contains = true;
                }
                if (contains)
                {
                    for (var j = 0; j < sections[i].Count; j++)
                    {
                        if (sections[i][j].Transit || sections[i][j].IsCrossing) sections[i][j].SectionModified = true;
                    }
                }
            }

            ITripManager m = ObjectContainer.GetTripManager();

            m.Save(user.ID, trip);
        }

        public void SetSectionAsNotModified(User user, Trip trip, Location location)
        {
            List<List<Location>> sections = SplitTripIntoSections(trip);
            for (var i = 0; i < sections.Count; i++)
            {
                var contains = false;
                for (var k = 0; k < sections[i].Count; k++)
                {
                    if (sections[i][k].ID == location.ID) contains = true;
                }
                if (contains)
                {
                    for (var j = 0; j < sections[i].Count; j++)
                    {
                        if (sections[i][j].Transit || sections[i][j].IsCrossing) sections[i][j].SectionModified = false;
                    }
                }
            }

            ITripManager m = ObjectContainer.GetTripManager();

            m.Save(user.ID, trip);
        }

        public async Task<Trip> FillBorderPoints(User user, Trip trip, Location editedLocation)
        {

            if (trip.Locations.Count < 3) return trip;

            trip.ArrangePoints();

            ITripManager m = ObjectContainer.GetTripManager();

            trip = m.SaveAndReturn(user.ID, trip);

            //--------------------------------------------

            List<List<Location>> sections = SplitTripIntoSections(trip);

            for (var j = 0; j < sections.Count; j++)
            {
                List<Location> section = sections[j];
                //section[1] is most probably always a border cross and it must always exist
                if (section[1].SectionModified) continue;

                EraseListLocationPoints(section);

                //This for loop should be useless, as at this point there is only one border-cross point in the section
                for (var i = 0; i < section.Count; i++)
                {
                    if (i == 0 || i == section.Count - 1) continue;
                    var l = section.ElementAt(i);
                    var ln = section.ElementAt(i + 1);
                    var lp = section.ElementAt(i - 1);
                    //if (l.ID != editedLocation.ID && ln.ID != editedLocation.ID && lp.ID != editedLocation.ID) continue;
                    //This partially solves the pointless filling, but it still checks for both inbound and outbound crosses, but it's not always necesarry!!!
                    if (l.IsCrossing && lp.DepartureDate.HasValue && lp.DepartureTime.HasValue && ln.ArrivalDate.HasValue && ln.ArrivalTime.HasValue && ln.InboundTravelType.HasValue)
                    {
                        l.CrossedBorder = lp.City.CountryID != ln.City.CountryID;
                        if (l.CrossedBorder)
                        {
                            BorderCrossObject o = await GetBorderCrossTime(lp, ln, section, i);
                            if (o != null)
                            {
                                long transitPointTime = Convert.ToInt64(o.CrossedAt % (60000 * 60 * 24));
                                long transitPointDate = Convert.ToInt64(o.CrossedAt - transitPointTime);
                                l.CrossedAtDate = transitPointDate;
                                l.CrossedAtTime = transitPointTime;
                                i += o.IncrementBy;
                            }
                            else
                            {
                                l.CrossedAtDate = -1;
                                l.CrossedAtTime = -1;
                            }
                        }
                    }
                    else
                    {
                        l.CrossedAt = null;
                    }
                }
            }

            trip.Locations = StichTogetherSections(sections);

            //----------------------------------------

            trip.Locations.OrderBy(x => x.Position);

            return trip;
        }

        public List<Location> StichTogetherSections(List<List<Location>> sections)
        {
            //The sections overlap
            List<Location> final = new List<Location>();
            for (var i = 0; i < sections.Count; i++)
            {
                List<Location> mod = sections[i];
                if (i < sections.Count - 1)
                {
                    mod.RemoveAt(mod.Count - 1);
                }
                final.AddRange(mod);
            }
            for (var i = 0; i < final.Count; i++)
            {
                final[i].Position = i;
            }

            return final;
        }

        public List<Location> EraseListLocationPoints(List<Location> locations)
        {

            ILocationDbProvider locationDbProvider = ObjectContainer.GetLocationDbProvider();

            for (var i = 0; i < locations.Count; i++)
            {
                if (locations[i].Transit)
                {
                    locationDbProvider.Delete(locations[i].ID);
                    locations.RemoveAt(i);
                    locationDbProvider.Delete(locations[i - 1].ID);
                    locations.RemoveAt(i - 1);
                    i -= 2;
                    //Order is important here
                }
            }

            return locations;
        }

        public Trip EraseTransitPoints(Trip trip)
        {
            for (var i = 0; i < trip.Locations.Count; i++)
            {
                if (trip.Locations[i].Transit)
                {
                    trip.Locations[i].Deleted = true;
                    trip.Locations[i - 1].Deleted = true;
                }
            }

            return trip;
        }
        
        public class BorderCrossObject
        {
            public long CrossedAt { get; set; }
            public int IncrementBy { get; set; }
        }

        public async Task<BorderCrossObject> GetBorderCrossTime(Location start, Location finish, List<Location> section, int insertAt)
        {

            if (finish.InboundTravelType == TravelType.PLANE)
            {
                return new BorderCrossObject { IncrementBy = 0, CrossedAt = start.DepartureTime.Value + start.DepartureDate.Value };
            }
            else if (finish.InboundTravelType == TravelType.CAR || finish.InboundTravelType == TravelType.PUBLIC_TRANSPORT || finish.InboundTravelType == TravelType.BOAT)
            {
                long departure = start.DepartureDate.Value + start.DepartureTime.Value;
                long arrival = finish.ArrivalDate.Value + finish.ArrivalTime.Value;

                if (arrival <= departure) return null;

                string departureId = start.City.GooglePlaceId;
                string arrivalId = finish.City.GooglePlaceId;

                HttpClient c = new HttpClient();
                string key = System.IO.File.ReadAllText(Path.GetFullPath("~/../Assets/api_key.txt").Replace("~\\", ""));
                HttpResponseMessage m;
                try
                {
                    m = await c.GetAsync("https://maps.googleapis.com/maps/api/directions/json?origin=place_id:" + departureId + "&destination=place_id:" + arrivalId + "&key=" + key);
                }
                catch (HttpRequestException e)
                {
                    return null;
                }

                string response = await m.Content.ReadAsStringAsync();

                MapParent d = Newtonsoft.Json.JsonConvert.DeserializeObject<MapParent>(response);

                if (d.Routes == null || d.Routes.Length == 0)
                {
                    return new BorderCrossObject { IncrementBy = 0, CrossedAt = -1 };
                }

                MapLeg[] legs = d.Routes[0].Legs;

                //Checking if there is more than one border cross
                int crosses = 0;
                //Count the looped seconds
                long counter = 0;
                List<long> durations = new List<long>();
                long totalTripMapLength = 0;
                List<string> countries = new List<string>();
                for (var i = 0; i < legs.Length; i++)
                {

                    totalTripMapLength += Convert.ToInt64(legs[i].Duration.Value);

                    for (var j = 0; j < legs[i].Steps.Length; j++)
                    {

                        counter += Convert.ToInt32(legs[i].Steps[j].Duration.Value);

                        if (legs[i].Steps[j].Html_Instructions.Contains("Entering ") && !legs[i].Steps[j].Html_Instructions.Contains("Entering toll zone"))
                        {
                            crosses++;
                            durations.Add(counter);
                            countries.Add(legs[i].Steps[j].Html_Instructions.Split("Entering ")[1].Split('<')[0]);
                        }
                    }
                }

                double ratio = Convert.ToDouble(totalTripMapLength * 1000) / Convert.ToDouble(arrival - departure);

                for (var i = 0; i < countries.Count; i++)
                {
                    countries[i] = MapsController.FixCountryNames(countries[i]);
                }

                if (countries.Count > 1)
                {

                    ICityDbProvider cityDbProvider = ObjectContainer.GetCityDbProvider();

                    for (var i = countries.Count - 2; i >= 0; i--)
                    {

                        City city = new City { Country = cityDbProvider.GetCountryByName(countries[i]), Name = "Transit_Country", CountryID = cityDbProvider.GetCountryByName(countries[i]).ID };

                        double transitPointDateTime = start.DepartureDate.Value + start.DepartureTime.Value + durations[i] * 1000 / ratio;

                        long transitPointTime = Convert.ToInt64(transitPointDateTime % (60000 * 60 * 24));
                        long transitPointDate = Convert.ToInt64(transitPointDateTime - transitPointTime);

                        section.Insert(insertAt, new Location { City = city, Transit = true, ArrivalDate = transitPointDate, ArrivalTime = transitPointTime, DepartureDate = transitPointDate, DepartureTime = transitPointTime, InboundTravelType = TravelType.CAR });
                        section.Insert(insertAt, new Location { IsCrossing = true, CrossedBorder = true, Transit = false, CrossedAtDate = transitPointDate, CrossedAtTime = transitPointTime });
                    }

                    //Make sure to save the trip!
                }
                //Done checking if there is more than one border cross

                for (var i = 0; i < section.Count; i++)
                {
                    section[i].Position = i;
                }

                return new BorderCrossObject
                {
                    IncrementBy = (countries.Count - 1) * 2,
                    CrossedAt = start.DepartureDate.Value + start.DepartureTime.Value + Convert.ToInt64(durations[durations.Count - 1] * 1000 / ratio)
                };              
            }
            else return null;

        }

        public Trip CheckForMissingTransitCountries(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
