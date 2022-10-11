using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace CalendarApp.Models
{
    public class Subject
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int NumOfLessons { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int Id { get; set; }
        public string Code { get; set; }
        public int StartTimeInt { get; set; }
        public int NumOfLessonsPerDay { get; set; }
        public int? NotiBeforeTime { get; set; }
        public string ColorCode { get; set; }
    }
}


