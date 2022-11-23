using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace CalendarApp.Models
{
    public class Subject
    {
        public string title { get; set; }
        public string description { get; set; }
        public string StartTimeUI { get; set; }
        public string EndTimeUI { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public int notiBeforeTime { get; set; }
        public DateTime startDate { get; set; }
        public string colorCode { get; set; }
        public string notiUnit { get; set; }
        public DateTime? endDate { get; set; }
        public int numOfLessons { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public int numOfLessonsPerDay { get; set; }
        public List<string> dayOfWeeks { get; set; }
        public List<DateTime> dayOffs { get; set; }
    }
}


