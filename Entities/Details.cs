using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMock.Entities
{
    public class Details
    {
        public string Description { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string MenuDay1 { get; set; }
        public string MenuDay2 { get; set; }
        public string MenuDay3 { get; set; }
        public string MenuDay4 { get; set; }
        public string MenuDay5 { get; set; }
        public string MenuDay6 { get; set; }
        public string MenuDay7 { get; set; }

        public Details(string description, string openTime, string closeTime, string menuDay1, string menuDay2, string menuDay3, string menuDay4, string menuDay5, string menuDay6, string menuDay7)
        {
            Description = description;
            OpenTime = openTime;
            CloseTime = closeTime;
            MenuDay1 = menuDay1;
            MenuDay2 = menuDay2;
            MenuDay3 = menuDay3;
            MenuDay4 = menuDay4;
            MenuDay5 = menuDay5;
            MenuDay6 = menuDay6;
            MenuDay7 = menuDay7;
        }
    }
}
