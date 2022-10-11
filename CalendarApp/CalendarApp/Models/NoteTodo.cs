using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class NoteTodo : BaseViewModel
    {
        private bool _isDone;
        public bool isDone
        {
            get { return _isDone; }
            set { _isDone = value; OnPropertyChanged(); }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
    }
}
