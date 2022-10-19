using CalendarApp.Models;
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
        public ICommand OpenAddTaskPopupCM { get; set; }

        private ObservableCollection<ObservableCollection<Subject>> subjects;
        public ObservableCollection<ObservableCollection<Subject>> Subjects
        {
            get { return subjects; }
            set { subjects = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Subject> todaySubject;
        public ObservableCollection<Subject> TodaySubject
        {
            get { return todaySubject; }
            set { todaySubject = value; OnPropertyChanged(); }
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
                    for (int i = 0; i < Days.Count; i++)
                    {
                        Days[i].IsSelected = false;
                    }
                    selectedDay = value;
                    GetWeekFromSelectedDay(selectedDay);
                    bool isTodayWeek = false;
                    for (int i = 0; i < Days.Count; i++)
                    {
                        if (Days[i].Day == DateTime.Now.Day)
                        {
                            isTodayWeek = true;
                        }
                    }
                    if (!isTodayWeek)
                    {
                        TodaySubject = Subjects[0];
                        Days[0].IsSelected = true;
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

        private void InitData()
        {
            SelectedDay = DateTime.Now;
            Subjects = new ObservableCollection<ObservableCollection<Subject>>();
            for (int i = 0; i < 6; i++)
            {
                ObservableCollection<Subject> temp = new ObservableCollection<Subject>();
                if (i == 0)
                    temp.Add(new Subject { StartTime = "7:30", EndTime = "9:45", Title = "Phát triển ứng dụng đa phương tiện trên thiết bị di động", Description = "Phòng B4.10, thày Phạm Nguyễn Trường An" });
                if (i == 1)
                    temp.Add(new Subject { StartTime = "7:30", EndTime = "9:00", Title = "Kinh tế chính trị Mác – Lênin", Description = "Phòng B6.06, thày Nguyễn Hữu Trinh" });
                if (i == 2)
                    temp.Add(new Subject { StartTime = "13:00", EndTime = "16:15", Title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động", Description = "Phòng B1.14, thày Võ Ngọc Tân" });
                if (i == 3)
                    temp.Add(new Subject { StartTime = "13:00", EndTime = "17:00", Title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động (TH)", Description = "Phòng C111, thày Phạm Nhật Duy" });
                if (i == 4)
                {
                    temp.Add(new Subject { StartTime = "9:00", EndTime = "11:30", Title = "Phương pháp Phát triển phần mềm hướng đối tượng", Description = "Phòng B1.16, cô Nguyễn Hồng Thủy" });
                    temp.Add(new Subject { StartTime = "13:00", EndTime = "14:30", Title = "Pháp luật đại cương", Description = "Phòng C205, cô Phạm Thị Thảo Xuyên" });
                }
                if (i == 5)
                {
                    temp.Add(new Subject { StartTime = "7:30", EndTime = "10:45", Title = "Công nghệ .NET", Description = "Phòng B6.02, cô Huỳnh Hồ Thị Mộng Trinh" });
                    temp.Add(new Subject { StartTime = "13:45", EndTime = "17:00", Title = "Quản lý dự án Phát triển Phần mềm", Description = "Phòng B1.18, cô Đỗ Thị Thanh Tuyền" });
                }
                Subjects.Add(temp);
            }

            GetWeekFromSelectedDay(DateTime.Now);

            for (int i = 0; i < Days.Count; i++)
            {
                if (Days[i].IsSelected)
                {
                    TodaySubject = Subjects[i];
                    break;
                }
            }

            LabelToday = $"{DateTime.Now.Day} {DateTime.Now:MMMM}";
        }

        public ScheduleViewModel()
        {
            InitData();

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
                                TodaySubject = Subjects[i];
                            }
                            else
                            {
                                TodaySubject = null;
                            }
                        }
                        else
                        {
                            Days[i].IsSelected = false;
                        }
                    }
            });

            OpenAddTaskPopupCM = new Command(() =>
            {
                App.Current.MainPage.Navigation.ShowPopup(new AddTaskPopup());
            });
        }
    }


}
