using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Task
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int StartTimeInt { get; set; }
        public int? NotiBeforeTime { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }
        public string ColorCode { get; set; }
        public string NotifyTimeString { get; set; }
    }
}







