using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Subject
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
