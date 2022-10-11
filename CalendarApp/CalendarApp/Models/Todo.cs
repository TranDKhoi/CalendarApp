using CalendarApp.ViewModels;
using System;


namespace CalendarApp.Models
{

    public class Todo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ColorCode { get; set; }

        public int StartTimeInt { get; set; }
        public int EndTimeInt { get; set; }
        public int? NotiBeforeTime { get; set; }
        //FOR UI ONLY
        public string NotifyTimeString { get; set; }
        //----------------------------------------
        public int? CourseId { get; set; }
        public Subject Subject { get; set; }

        public DateTime? RecurringStart { get; set; }
        public int? RecurringInterval { get; set; }
        public DateTime? RecurringEnd { get; set; }

    }
}
