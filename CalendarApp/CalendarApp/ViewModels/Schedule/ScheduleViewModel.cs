using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CalendarApp.ViewModels.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        private ObservableCollection<DayTitle> _days;
        public ObservableCollection<DayTitle> days
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(); }
        }

        public ScheduleViewModel()
        {
            days = new ObservableCollection<DayTitle>();
            days.Add(new DayTitle { title = "Hai", day = 26 });
            days.Add(new DayTitle { title = "Ba", day = 27 });
            days.Add(new DayTitle { title = "Tư", day = 28 });
            days.Add(new DayTitle { title = "Năm", day = 29 });
            days.Add(new DayTitle { title = "Sáu", day = 30 });
            days.Add(new DayTitle { title = "Bảy", day = 1 });
            days.Add(new DayTitle { title = "CN", day = 2 });
        }
    }
    public class DayTitle
    {
        public string title { get; set; }
        public int day { get; set; }
    }
}
