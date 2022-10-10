using CalendarApp.Models;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

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

        private string today;
        public string Today
        {
            get { return today; }
            set { today = value; OnPropertyChanged(); }
        }

        public ScheduleViewModel()
        {
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
            TodaySubject = Subjects[0];

            Days = new ObservableCollection<DayTitle>();
            Days.Add(new DayTitle { Title = "Hai", Day = 26, IsSelected = true });
            Days.Add(new DayTitle { Title = "Ba", Day = 27, IsSelected = false });
            Days.Add(new DayTitle { Title = "Tư", Day = 28, IsSelected = false });
            Days.Add(new DayTitle { Title = "Năm", Day = 29, IsSelected = false });
            Days.Add(new DayTitle { Title = "Sáu", Day = 30, IsSelected = false });
            Days.Add(new DayTitle { Title = "Bảy", Day = 1, IsSelected = false });
            Days.Add(new DayTitle { Title = "CN", Day = 2, IsSelected = false });
            Today = $"{DateTime.Now.Day} {DateTime.Now:MMMM}";

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
