using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalendarApp.Models
{
    public class DayTitle : INotifyPropertyChanged
    {
        public string title { get; set; }
        public int day { get; set; }
        private bool _isSelected { get; set; }
        public bool isSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isSelected)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
