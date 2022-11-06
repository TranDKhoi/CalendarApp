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
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace CalendarApp.ViewModels.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        public ICommand SelectDayCM { get; set; }
        public ICommand SelectTaskCM { get; set; }
        public ICommand OpenAddTaskPopupCM { get; set; }
        public ICommand GetAllTaskCM { get; set; }

        private ObservableCollection<Task> todayTask;
        public ObservableCollection<Task> TodayTask
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
                        temp.Day = selectedDay.Day;
                        break;
                    case 3:
                        temp.Title = "Ba";
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 4:
                        temp.Title = "Tư";
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 5:
                        temp.Title = "Năm";
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 6:
                        temp.Title = "Sáu";
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 7:
                        temp.Title = "Bảy";
                        temp.IsSelected = false;
                        temp.Day = selectedDay.Day;
                        break;
                    case 8:
                        temp.Title = "CN";
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
                    TimeSpan t2;
                    TimeSpan t1 = TimeSpan.FromSeconds(TodayTask[i].startTime);
                    if (TodayTask[i] is Todo)
                    {
                        var temp = (Todo)TodayTask[i];
                        t2 = TimeSpan.FromSeconds(temp.endTime);
                    }
                    else
                    {
                        var temp = (Subject)TodayTask[i];
                        t2 = TimeSpan.FromSeconds(temp.startTime + temp.numOfLessonsPerDay * 45 * 60);
                    }
                    string answer1 = string.Format("{0:D2}:{1:D2}", t1.Hours, t1.Minutes);
                    string answer2 = string.Format("{0:D2}:{1:D2}", t2.Hours, t2.Minutes);
                    TodayTask[i].StartTimeUI = answer1;
                    TodayTask[i].EndTimeUI = answer2;
                }
            }
        }

        private void InitData()
        {
            SelectedDay = DateTime.Now;

            GetWeekFromSelectedDay(DateTime.Now);

            for (int i = 0; i < Days.Count; i++)
            {
                if (Days[i].IsSelected)
                {
                    //TodayTask = Tasks[i];
                    CalculateStartEndTime();
                    break;
                }
            }

            LabelToday = $"{DateTime.Now.Day} {DateTime.Now:MMMM}";
        }

        public ScheduleViewModel()
        {
            GetAllTaskCM = new Command(() =>
            {
                InitData();
            });

            SelectDayCM = new Command((p) =>
            {
                if (p is DayTitle)
                    for (int i = 0; i < Days.Count; i++)
                    {
                        if (Days[i] == p)
                        {
                            Days[i].IsSelected = true;
                            if (i < 6)
                            {
                                //TodayTask = Tasks[i];
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
                var selectedTask = p as Task;
                if (selectedTask is Todo)
                {
                    App.Current.MainPage.Navigation.PushAsync(new EditTodoScreen(selectedTask as Todo));
                }
                else if (selectedTask is Subject)
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

                    }

                }
            });
        }
    }


}
