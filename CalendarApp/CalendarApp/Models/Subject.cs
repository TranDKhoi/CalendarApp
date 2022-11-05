using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace CalendarApp.Models
{
    public class Subject : Task
    {
        public DateTime? endDate { get; set; }
        public int numOfLessons { get; set; }

        public int id { get; set; }
        public string code { get; set; }
        public int numOfLessonsPerDay { get; set; }
        public List<string> dayOfWeeks { get; set; }
    }
}


