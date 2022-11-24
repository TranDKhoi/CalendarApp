using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;

namespace CalendarApp.Models
{

    public class Event:BaseViewModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public string baseEventId { get; set; }
        public string cloneEventId { get; set; }
        public string notiUnit { get; set; }
        public string recurringUnit { get; set; }
        public string StartTimeUI { get; set; }
        private String endTimeUI;
        public String EndTimeUI
        {
            get { return endTimeUI; }
            set { endTimeUI = value; OnPropertyChanged(); }
        }

        public int? courseId { get; set; }
        public int? notiBeforeTime { get; set; }
        public DateTime startTime { get; set; }
        public DateTime beforeStartTime { get; set; }
        public DateTime endTime { get; set; }
        public List<string> recurringDetails { get; set; }
        public DateTime? recurringStart { get; set; }
        public int? recurringInterval { get; set; }
        public DateTime? recurringEnd { get; set; }
        public string targetType { get; set; }

    }
}
