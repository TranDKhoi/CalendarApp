using CalendarApp.Models;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Schedule
{
    public class SelectDayOfWeekViewModel : BaseViewModel
    {
        public ICommand SelectDayCM { get; set; }
        public ICommand DoneCM { get; set; }

        private DateTime today;
        public DateTime Today
        {
            get { return today; }
            set
            {
                today = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DayTitle> days;
        public ObservableCollection<DayTitle> Days
        {
            get { return days; }
            set { days = value; OnPropertyChanged(); }
        }

        private List<DayTitle> selectedWeekDay;
        public List<DayTitle> SelectedWeekDay
        {
            get { return selectedWeekDay; }
            set { selectedWeekDay = value; OnPropertyChanged(); }
        }

        private void InitData()
        {
            SelectedWeekDay = new List<DayTitle>();
            Days = new ObservableCollection<DayTitle>
            {
                new DayTitle { Title = "T2", IsSelected = false , Detail="Thứ hai ", DetailEng="Monday",},
                new DayTitle { Title = "T3", IsSelected = false ,Detail="Thứ ba ", DetailEng="Tuesday",},
                new DayTitle { Title = "T4", IsSelected = false ,Detail="Thứ tư ",DetailEng="Wednesday",},
                new DayTitle { Title = "T5", IsSelected = false ,Detail="Thứ năm ",DetailEng="Thursday",},
                new DayTitle { Title = "T6", IsSelected = false ,Detail="Thứ sáu ",DetailEng="Friday",},
                new DayTitle { Title = "T7", IsSelected = false ,Detail="Thứ bảy ",DetailEng="Saturday",},
                new DayTitle { Title = "Cn", IsSelected = false ,Detail="Chủ nhật ",DetailEng="Sunday",},
            };

        }

        public SelectDayOfWeekViewModel()
        {
            InitData();

            SelectDayCM = new Command((p) =>
            {
                if (p != null)
                {
                    DayTitle dayTitle = p as DayTitle;
                    dayTitle.IsSelected = !dayTitle.IsSelected;
                    if (dayTitle.IsSelected)
                    {
                        SelectedWeekDay.Add(dayTitle);
                    }
                }
            });

            DoneCM = new Command((p) =>
            {
                if (p != null)
                {
                    if(SelectedWeekDay.Count == 0)
                    {
                        App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn thứ", "Đóng");
                    }
                    else
                    {
                        SelectDayOfWeekPopup popup = p as SelectDayOfWeekPopup;
                        SelectedWeekDay = SelectedWeekDay.OrderBy(q => q.Title).ToList();
                        popup.Dismiss(SelectedWeekDay);
                    }
                }
            });
        }
    }
}
