using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        public ICommand SelectDayCM { get; set; }

        private ObservableCollection<ObservableCollection<Subject>> _subjects;
        public ObservableCollection<ObservableCollection<Subject>> subjects
        {
            get { return _subjects; }
            set { _subjects = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Subject> _todaySubject;
        public ObservableCollection<Subject> todaySubject
        {
            get { return _todaySubject; }
            set { _todaySubject = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DayTitle> _days;
        public ObservableCollection<DayTitle> days
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(); }
        }

        private string _today;
        public string today
        {
            get { return _today; }
            set { _today = value; OnPropertyChanged(); }
        }

        public ScheduleViewModel()
        {
            subjects = new ObservableCollection<ObservableCollection<Subject>>();
            for (int i = 0; i < 6; i++)
            {
                ObservableCollection<Subject> temp = new ObservableCollection<Subject>();
                if (i == 0)
                    temp.Add(new Subject { startTime = "7:30", endTime = "9:45", title = "Phát triển ứng dụng đa phương tiện trên thiết bị di động", description = "Phòng B4.10, thày Phạm Nguyễn Trường An" });
                if (i == 1)
                    temp.Add(new Subject { startTime = "7:30", endTime = "9:00", title = "Kinh tế chính trị Mác – Lênin", description = "Phòng B6.06, thày Nguyễn Hữu Trinh" });
                if (i == 2)
                    temp.Add(new Subject { startTime = "13:00", endTime = "16:15", title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động", description = "Phòng B1.14, thày Võ Ngọc Tân" });
                if (i == 3)
                    temp.Add(new Subject { startTime = "13:00", endTime = "17:00", title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động (TH)", description = "Phòng C111, thày Phạm Nhật Duy" });
                if (i == 4)
                {

                    temp.Add(new Subject { startTime = "9:00", endTime = "11:30", title = "Phương pháp Phát triển phần mềm hướng đối tượng", description = "Phòng B1.16, cô Nguyễn Hồng Thủy" });
                    temp.Add(new Subject { startTime = "13:00", endTime = "14:30", title = "Pháp luật đại cương", description = "Phòng C205, cô Phạm Thị Thảo Xuyên" });
                }
                if (i == 5)
                {

                    temp.Add(new Subject { startTime = "7:30", endTime = "10:45", title = "Công nghệ .NET", description = "Phòng B6.02, cô Huỳnh Hồ Thị Mộng Trinh" });
                    temp.Add(new Subject { startTime = "13:45", endTime = "17:00", title = "Quản lý dự án Phát triển Phần mềm", description = "Phòng B1.18, cô Đỗ Thị Thanh Tuyền" });
                }
                subjects.Add(temp);
            }
            todaySubject = subjects[0];

            days = new ObservableCollection<DayTitle>();
            days.Add(new DayTitle { title = "Hai", day = 26, isSelected = true });
            days.Add(new DayTitle { title = "Ba", day = 27, isSelected = false });
            days.Add(new DayTitle { title = "Tư", day = 28, isSelected = false });
            days.Add(new DayTitle { title = "Năm", day = 29, isSelected = false });
            days.Add(new DayTitle { title = "Sáu", day = 30, isSelected = false });
            days.Add(new DayTitle { title = "Bảy", day = 1, isSelected = false });
            days.Add(new DayTitle { title = "CN", day = 2, isSelected = false });
            today = $"{DateTime.Now.Day} {DateTime.Now:MMMM}";

            SelectDayCM = new Command((p) =>
            {
                if (p is DayTitle)
                    for (int i = 0; i < days.Count; i++)
                    {
                        if (days[i] == p)
                        {
                            days[i].isSelected = true;
                            if (i < 6)
                            {
                                todaySubject = subjects[i];
                            }
                            else
                            {
                                todaySubject = null;
                            }
                        }
                        else
                        {
                            days[i].isSelected = false;
                        }
                    }
            });
        }
    }


}
