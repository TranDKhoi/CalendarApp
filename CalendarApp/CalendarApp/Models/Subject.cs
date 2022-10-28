using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace CalendarApp.Models
{
    public class Subject : Task
    {
        public DateTime? EndDate { get; set; }
        public int NumOfLessons { get; set; }

        public int Id { get; set; }
        public string Code { get; set; }
        public int NumOfLessonsPerDay { get; set; }
    }
}


