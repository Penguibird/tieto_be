using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class DayFood
    {
        public int ID { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }

        public DayFood()
        {

        }

        public DayFood(bool breakfast, bool lunch, bool dinner)
        {
            Breakfast = breakfast;
            Lunch = lunch;
            Dinner = dinner;
        }

        public DayFood(DayFood template)
        {
            Breakfast = template.Breakfast;
            Lunch = template.Lunch;
            Dinner = template.Dinner;
        }
    }
}
