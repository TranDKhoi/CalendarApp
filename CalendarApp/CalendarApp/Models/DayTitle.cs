using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalendarApp.Models
{
    public class DayTitle : BaseViewModel
    {
        public string Title { get; set; }
        public int Day { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(); }
        }
    }
}
