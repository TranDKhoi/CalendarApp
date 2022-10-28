using CalendarApp.ViewModels;
using System;


namespace CalendarApp.Models
{

    public class Todo : Task
    {
        public int EndTimeInt { get; set; }
        //FOR UI ONLY
        //----------------------------------------
        public int? CourseId { get; set; }
        public Subject Subject { get; set; }

        public DateTime? RecurringStart { get; set; }
        public int? RecurringInterval { get; set; }
        public DateTime? RecurringEnd { get; set; }

    }
}
