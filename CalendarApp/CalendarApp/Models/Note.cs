using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Note : BaseViewModel
    {
        public string title { get; set; }
        public DateTime createdAt { get; set; }
        private List<NoteTodo> _noteTodo;
        public List<NoteTodo> noteTodo
        {
            get { return _noteTodo; }
            set { _noteTodo = value; OnPropertyChanged(); }
        }
        public string content { get; set; }
    }
}
