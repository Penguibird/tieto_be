using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Trip : IDisposable
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Purpose { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public string Comment { get; set; }
        public bool Deleted { get; set; }
        public bool Exported { get; set; }
        public DateTime? StartDate { get; set; }
        public DayExchange Exchange { get; set; }
        //Always create new DayExchanges when assigning them to trips, as they may be modified!
        public IList<Location> Locations { get; set; }
        public int UserID { get; set; }

        public void ArrangePoints()
        {
            Locations = Locations.OrderBy(x => x.Position).ToList();
            for (var i = 0; i < Locations.Count; i++)
            {
                if (Locations[i].Position > i)
                {
                    Locations[i].Position = i;
                }
            }
            for (var i = 0; i < Locations.Count; i++)
            {
                if (i != Locations[i].Position) {
                    Locations.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Dispose()
        {
            return;
        }
    }
}
