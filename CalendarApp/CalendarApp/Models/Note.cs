using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Note
    {
        public string title { get; set; }
        public DateTime createdAt { get; set; }
        public List<NoteTodo> noteTodo { get; set; }
        public string content { get; set; }
        public string recurringUnit { get; set; }
    }
}
