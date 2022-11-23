using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class DayOffSubject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime offDate { get; set; }
        public string StartTimeUI { get; set; }
        public string EndTimeUI { get; set; }
        public string colorCode { get; set; }
    }
}
