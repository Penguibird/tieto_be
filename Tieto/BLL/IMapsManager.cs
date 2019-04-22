using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;
using static Tieto.BLL.MapsManager;

namespace Tieto.BLL
{
    public interface IMapsManager
    {
        Task<Trip> FillBorderPoints(User user, Trip trip, Location editedLocation);

        Task<BorderCrossObject> GetBorderCrossTime(Location start, Location finish, List<Location> section, int insertAt);

        Trip CheckForMissingTransitCountries(Trip trip);

        List<List<Location>> SplitTripIntoSections(Trip trip);

        void SetSectionAsModified(User user, Trip trip, Location location);

        void SetSectionAsNotModified(User user, Trip trip, Location location);

        List<Location> GetSectionFromLocation(Trip trip, Location location);
    }
}
