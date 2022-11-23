using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views.Manage;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace CalendarApp.ViewModels.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        public Command SelectDayCM { get; set; }
        public Command SelectTaskCM { get; set; }
        public Command OpenAddTaskPopupCM { get; set; }
        public Command GetAllTaskCM { get; set; }
        public Command SelectRestDayCM { get; set; }

        private ObservableCollection<Event> todayTask;
        public ObservableCollection<Event> TodayTask
        {
            get { return todayTask; }
            set { todayTask = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DayTitle> days;
        public ObservableCollection<DayTitle> Days
        {
            get { return days; }
            set { days = value; OnPropertyChanged(); }
        }

        private DateTime selectedDay;
        public DateTime SelectedDay
        {
            get { return selectedDay; }
            set
            {
                if (Days != null)
                {
                    selectedDay = value;
                    GetWeekFromSelectedDay(selectedDay);
                    for (int i = 0; i < Days.Count; i++)
                    {
                        Days[i].IsSelected = false;
                    }
                    for (int i = 0; i < Days.Count; i++)
                    {
                        if (selectedDay.Day == Days[i].Day)
                        {
                            SelectDayCM.Execute(Days[i]);

                        }
                    }
                }
                else
                {
                    selectedDay = value;
                }
                OnPropertyChanged();
            }
        }

        private string labelToday;
        public string LabelToday
        {
            get { return labelToday; }
            set { labelToday = value; OnPropertyChanged(); }
        }

        private void GetWeekFromSelectedDay(DateTime selectedDay)
        {
            while (new CultureInfo("vi-VN").DateTimeFormat.GetDayName(selectedDay.DayOfWeek) != "Thứ Hai")
            {
                selectedDay = selectedDay.AddDays(-1);
            }
            Days = new ObservableCollection<DayTitle>();
            for (int i = 2; i < 9; i++)
            {
                var temp = new DayTitle();
                switch (i)
                {
                    case 2:
                        temp.Title = "Hai";
                        temp.IsSelected = false;
                        temp.FullDate = selectedDay;
                        temp.Day = selectedDay.Day;
                        break;
                    case 3:
                        temp.Title = "Ba";
                        temp.IsSelected = false;
                        temp.FullDate = selectedDay;
                        temp.Day = selectedDay.Day;
                        break;
                    case 4:
                        temp.Title = "Tư";
                        temp.IsSelected = false;
                        temp.FullDate = selectedDay;
                        temp.Day = selectedDay.Day;
                        break;
                    case 5:
                        temp.Title = "Năm";
                        temp.FullDate = selectedDay;
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 6:
                        temp.Title = "Sáu";
                        temp.FullDate = selectedDay;
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 7:
                        temp.Title = "Bảy";
                        temp.FullDate = selectedDay;
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 8:
                        temp.Title = "CN";
                        temp.FullDate = selectedDay;
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    default:
                        break;
                }
                if (selectedDay.Day == DateTime.Now.Day)
                {
                    temp.IsSelected = true;
                }
                Days.Add(temp);
                selectedDay = selectedDay.AddDays(1);
            }
        }

        private void CalculateStartEndTime()
        {
            if (TodayTask != null)
            {
                for (int i = 0; i < TodayTask.Count; i++)
                {
                    TodayTask[i].StartTimeUI = TodayTask[i].startTime.ToString("HH:mm");
                    TodayTask[i].EndTimeUI = TodayTask[i].endTime.ToString("HH:mm");
                }
            }
        }

        private async Task InitDataAsync()
        {
            SelectedDay = DateTime.Now;

            GetWeekFromSelectedDay(DateTime.Now);

            for (int i = 0; i < Days.Count; i++)
            {
                if (Days[i].IsSelected)
                {

                    var res = await EventService.ins.GetAllTaskByDay(DateTime.Now);
                    if (res.isSuccess)
                    {
                        TodayTask = new ObservableCollection<Event>(res.data);
                    }
                    CalculateStartEndTime();
                    break;
                }
            }

            LabelToday = $"{DateTime.Now.Day} {DateTime.Now:MMMM}";
        }

        public ScheduleViewModel()
        {
            GetAllTaskCM = new Command(async () =>
            {
                await InitDataAsync();
            });

            SelectDayCM = new Command(async (p) =>
            {
                if (p is DayTitle)
                    for (int i = 0; i < Days.Count; i++)
                    {
                        if (Days[i] == p)
                        {
                            Days[i].IsSelected = true;
                            if (i < 6)
                            {
                                var res = await EventService.ins.GetAllTaskByDay(Days[i].FullDate);
                                if (res.isSuccess)
                                {
                                    TodayTask = new ObservableCollection<Event>(res.data);
                                }
                                CalculateStartEndTime();
                            }
                            else
                            {
                                TodayTask = null;
                            }
                        }
                        else
                        {
                            Days[i].IsSelected = false;
                        }
                    }
                CalculateStartEndTime();
            });

            SelectTaskCM = new Command((p) =>
            {
                if (p == null) return;
                Event e = p as Event;
                if (e.courseId == null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new EditTodoScreen(e));
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Thông báo", "Cần qua mục môn học để chỉnh sửa môn học này", "Đóng");
                }
            });

            OpenAddTaskPopupCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new AddTaskPopup());
                if (res != null)
                {
                    if (res.GetType() == typeof(Subject))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var result = await CourseService.ins.CreateNewCourse(res as Subject);
                        UserDialogs.Instance.HideLoading();

                        if (result.isSuccess)
                        {
                            _ = App.Current.MainPage.DisplayAlert("Thành công", "Thêm môn học thành công", "Đóng");
                            GetAllTaskCM.Execute(null);
                        }
                        else
                        {
                            UserDialogs.Instance.Toast(result.message);
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.ShowLoading();
                        var result = await EventService.ins.CreateNewTask(res as Event);
                        UserDialogs.Instance.HideLoading();

                        if (result.isSuccess)
                        {
                            _ = App.Current.MainPage.DisplayAlert("Thành công", "Thêm thành công", "Đóng");
                            GetAllTaskCM.Execute(null);
                        }
                        else
                        {
                            UserDialogs.Instance.Toast(result.message);
                        }
                    }

                }
            });

            SelectRestDayCM = new Command(async (p) =>
            {
                if (p == null) return;
                Event e = p as Event;
                bool result = await App.Current.MainPage.DisplayAlert("Nghỉ học", "Đánh dấu buổi này nghỉ", "Có", "Không");
                if (result)
                {
                    //Gọi api báo nghỉ
                }
                return;
            });
        }
    }


}
