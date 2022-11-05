using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Task
    {
        public string title { get; set; }

        public string description { get; set; }
        public DateTime startDate { get; set; }
        public int? notiBeforeTime { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string StartTimeUI { get; set; }
        public string EndTimeUI { get; set; }
        public string colorCode { get; set; }
        public string NotifyTimeString { get; set; }
    }
}







