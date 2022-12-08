﻿using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views.Manage;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Manage
{
    public class ManageViewModel : BaseViewModel
    {
        private ObservableCollection<Subject> listSubject;
        public ObservableCollection<Subject> ListSubject
        {
            get { return listSubject; }
            set { listSubject = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DayOffSubject> listDayOff;
        public ObservableCollection<DayOffSubject> ListDayOff
        {
            get { return listDayOff; }
            set { listDayOff = value; OnPropertyChanged(); }
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set { selectedSubject = value; OnPropertyChanged(); }
        }

        private string searchSubjectText;
        public string SearchSubjectText
        {
            get { return searchSubjectText; }
            set { searchSubjectText = value; OnPropertyChanged(); }
        }

        private string searchDayOffText;
        public string SearchDayOffText
        {
            get { return searchDayOffText; }
            set { searchDayOffText = value; OnPropertyChanged(); }
        }

        // data tra ve cho thg dat
        private List<string> selectedWeekDay;
        public List<string> SelectedWeekDay
        {
            get { return selectedWeekDay; }
            set { selectedWeekDay = value; }
        }

        //=====================================================
        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; OnPropertyChanged(); }
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged(); }
        }

        private int? lessonPerDay;
        public int? LessonPerDay
        {
            get { return lessonPerDay; }
            set { lessonPerDay = value; OnPropertyChanged(); }
        }

        private bool haveEndDate;
        public bool HaveEndDate
        {
            get { return haveEndDate; }
            set { haveEndDate = value; OnPropertyChanged(); }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged(); }
        }

        private int? totalLesson;
        public int? TotalLesson
        {
            get { return totalLesson; }
            set { totalLesson = value; OnPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> reminders;
        public ObservableCollection<string> Reminders
        {
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

        private string timeRemind;
        public string TimeRemind
        {
            get { return timeRemind; }
            set { timeRemind = value; OnPropertyChanged(); }
        }

        private Color colorTag;
        public Color ColorTag
        {
            get { return colorTag; }
            set { colorTag = value; OnPropertyChanged(); }
        }


        private string startTimeX;
        public string StartTimeX
        {
            get { return startTimeX; }
            set { startTimeX = value; OnPropertyChanged(); }
        }

        private string startTimeY;
        public string StartTimeY
        {
            get { return startTimeY; }
            set { startTimeY = value; OnPropertyChanged(); }
        }

        private string weekDayLabel;
        public string WeekDayLabel
        {
            get { return weekDayLabel; }
            set { weekDayLabel = value; OnPropertyChanged(); }
        }


        public Command FirstLoadSubjectCM { get; set; }
        public Command ToEditSubjectCM { get; set; }
        public Command UpdateSubjectCM { get; set; }
        public Command DeleteSubjectCM { get; set; }
        public Command OpenCustomReminderPopupCM { get; set; }
        public Command OpenColorPickerCM { get; set; }
        public Command OpenSelectDayOfWeekPopupCM { get; set; }
        public Command SearchSubjectCM { get; set; }
        public Command SearchDayOffCM { get; set; }
        public Command DeleteDayOffCM { get; set; }

        public ManageViewModel()
        {

            FirstLoadSubjectCM = new Command(async () =>
            {
                try
                {
                    SelectedSubject = null;
                    ClearSubjectData();

                    UserDialogs.Instance.ShowLoading();
                    var res = await CourseService.ins.GetAllCourse();
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        ListSubject = new ObservableCollection<Subject>(res.data);
                        ListDayOff = new ObservableCollection<DayOffSubject>();


                        for (int i = 0; i < ListSubject.Count; i++)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(ListSubject[i].startTime);
                            ListSubject[i].StartTimeUI = string.Format("{0:D1}:{1:D2}", t.Hours, t.Minutes);

                            TimeSpan t2 = TimeSpan.FromSeconds(ListSubject[i].startTime + 45 * 60 * ListSubject[i].numOfLessonsPerDay);
                            ListSubject[i].EndTimeUI = string.Format("{0:D1}:{1:D2}", t2.Hours, t2.Minutes);

                            //create list day off
                            if (ListSubject[i].dayOffs != null || ListSubject[i].dayOffs.Count != 0)
                            {
                                for (int j = 0; j < ListSubject[i].dayOffs.Count; j++)
                                {
                                    if (ListSubject[i].dayOffs[j] < DateTime.Now)
                                    {
                                        continue;
                                    }
                                    DayOffSubject dayOffSubject = new DayOffSubject
                                    {
                                        id = ListSubject[i].id,
                                        title = ListSubject[i].title,
                                        description = ListSubject[i].description,
                                        StartTimeUI = ListSubject[i].StartTimeUI,
                                        EndTimeUI = ListSubject[i].EndTimeUI,
                                        date = ListSubject[i].dayOffs[j],
                                        colorCode = ListSubject[i].colorCode,
                                    };
                                    ListDayOff.Add(dayOffSubject);
                                }
                            }
                        }
                        ListDayOff = new ObservableCollection<DayOffSubject>(ListDayOff.OrderBy(item => item.date).ToList());


                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
                catch (Exception)
                {
                }
                

            });
            ToEditSubjectCM = new Command(() =>
            {
                try
                {
                    ApplyDataToSubject();
                    App.Current.MainPage.Navigation.PushAsync(new EditSubjectScreen(this));
                }
                catch (Exception)
                {
                }
                
            });
            UpdateSubjectCM = new Command(async () =>
            {
                try
                {
                    if (!IsValidData())
                    {
                        return;
                    }
                    if (LessonPerDay == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng nhập số tiết", "Đóng");
                        return;
                    }

                    Subject subject = new Subject();
                    if (HaveEndDate)
                    {
                        if (!CheckTime(StartTimeY))
                        {
                            return;
                        }
                        // Gắn data trả về
                        subject = new Subject
                        {
                            id = SelectedSubject.id,
                            title = TaskName,
                            //prop code here
                            //===
                            description = Description ?? "",
                            dayOfWeeks = SelectedWeekDay,
                            numOfLessonsPerDay = (int)LessonPerDay,
                            startDate = StartDate,
                            endDate = EndDate,
                            startTime = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                            notiBeforeTime = GetRemindTime(),
                            colorCode = ColorTag.ToHexRgbString(),
                            notiUnit = GetRemindTimeUnit(),
                        };
                    }
                    else
                    {
                        if (TotalLesson == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng nhập tổng số tiết", "Đóng");
                            return;
                        }
                        if (TotalLesson < LessonPerDay)
                        {
                            await App.Current.MainPage.DisplayAlert("Cảnh báo", "Tổng số tiết không được nhỏ hơn số tiết 1 buổi", "Đóng");
                            return;
                        }
                        if (TotalLesson % LessonPerDay != 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Cảnh báo", "Tổng số tiết không chia hết cho số tiết một ngày", "Đóng");
                            return;
                        }
                        // Gắn data trả về
                        subject = new Subject
                        {
                            id = SelectedSubject.id,
                            title = TaskName,
                            //prop code here
                            //===
                            description = Description ?? "",
                            startTime = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                            dayOfWeeks = SelectedWeekDay,
                            numOfLessonsPerDay = (int)LessonPerDay,
                            startDate = StartDate,
                            numOfLessons = (int)TotalLesson,
                            notiBeforeTime = GetRemindTime(),
                            colorCode = ColorTag.ToHexRgbString(),
                            notiUnit = GetRemindTimeUnit(),
                        };
                    }

                    UserDialogs.Instance.ShowLoading();
                    var res = await CourseService.ins.UpdateCourse(subject);
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Thành công", "Cập nhật môn học thành công", "Đóng");
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
                catch (Exception)
                {
                }
                

            });
            DeleteSubjectCM = new Command(async () =>
            {
                try
                {
                    var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có chắc muốn xoá môn học này không?", "Có", "Không");
                    if (res)
                    {
                        UserDialogs.Instance.ShowLoading();
                        var result = await CourseService.ins.DeleteCourse(SelectedSubject.id);

                        if (result)
                        {
                            _ = App.Current.MainPage.Navigation.PopAsync();
                            UserDialogs.Instance.Toast("Xoá thành công");
                        }
                        else
                        {
                            UserDialogs.Instance.Toast("Lỗi hệ thống");
                        }
                    }
                }
                catch (Exception)
                {
                }
                
            });
            SearchSubjectCM = new Command((p) =>
            {
                try
                {
                    CollectionView cl = p as CollectionView;

                    ObservableCollection<Subject> search = new ObservableCollection<Subject>();

                    if (!string.IsNullOrEmpty(SearchSubjectText))
                    {

                        for (int i = 0; i < ListSubject.Count; i++)
                        {
                            if (ListSubject[i].title.ToLower().Contains(SearchSubjectText.ToLower()))
                            {
                                search.Add(ListSubject[i]);
                            }
                        }
                        cl.ItemsSource = search;
                    }
                    else
                    {
                        cl.ItemsSource = ListSubject;
                    }
                }
                catch (Exception)
                {
                }
                
            });
            OpenCustomReminderPopupCM = new Command(async () =>
            {
                try
                {
                    var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new CustomReminderPopup());
                    if (res != null)
                    {
                        if (!Reminders.Contains(res.ToString()))
                            Reminders.Add(res.ToString());
                        TimeRemind = res.ToString();
                    }
                }
                catch (Exception)
                {
                }
                
            });
            OpenColorPickerCM = new Command(async () =>
            {
                try
                {
                    var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new ColorPicker());
                    if (res != null)
                    {
                        ColorTag = (Color)res;
                    }
                }
                catch (Exception)
                {
                }
                
            });
            OpenSelectDayOfWeekPopupCM = new Command(async (p) =>
            {
                try
                {
                    if (p != null)
                    {
                        var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new SelectDayOfWeekPopup());
                        if (res != null)
                        {
                            List<DayTitle> selectedWeekDay = res as List<DayTitle>;
                            string temp = "";
                            List<string> tempList = new List<string>();
                            for (int i = 0; i < selectedWeekDay.Count; i++)
                            {
                                temp += selectedWeekDay[i].Detail;
                                if (i < selectedWeekDay.Count - 1)
                                {
                                    temp += ", ";
                                }
                                tempList.Add(selectedWeekDay[i].DetailEng);
                            }
                            WeekDayLabel = temp;
                            SelectedWeekDay = tempList;
                        }
                    }
                }
                catch (Exception)
                {
                }
                

            });

            DeleteDayOffCM = new Command(async (p) =>
            {
                try
                {
                    if (p == null) return;
                    DayOffSubject dayOffSubject = p as DayOffSubject;
                    bool result = await App.Current.MainPage.DisplayAlert("Thay đổi", "Xác nhận buổi này không nghỉ?", "Có", "Không");
                    if (result)
                    {
                        //Gọi api xóa ngày nghỉ
                        dayOffSubject.action = "Delete";
                        await CourseService.ins.ChangeSubjectStatus(dayOffSubject, dayOffSubject.id);
                        try
                        {
                            _ = App.Current.MainPage.DisplayAlert("Thành công", "Đánh dấu buổi nghỉ thành công", "Đóng");
                            ListDayOff.Remove(dayOffSubject);
                        }
                        catch (Exception e)
                        {
                            _ = App.Current.MainPage.DisplayAlert("Thất bại", $"Lỗi: {e}", "Đóng");
                        }
                    }
                    return;
                }
                catch (Exception)
                {
                }
                
            });

            SearchDayOffCM = new Command((p) =>
            {
                try
                {
                    CollectionView cl = p as CollectionView;

                    ObservableCollection<DayOffSubject> search = new ObservableCollection<DayOffSubject>();

                    if (!string.IsNullOrEmpty(SearchDayOffText))
                    {

                        for (int i = 0; i < ListDayOff.Count; i++)
                        {
                            if (ListDayOff[i].title.ToLower().Contains(SearchDayOffText.ToLower()))
                            {
                                search.Add(ListDayOff[i]);
                            }
                        }
                        search = new ObservableCollection<DayOffSubject>(search.OrderBy(item => item.date).ToList());
                        cl.ItemsSource = search;
                    }
                    else
                    {
                        cl.ItemsSource = ListDayOff;
                    }
                }
                catch (Exception)
                {
                }
                
            });
        }

        private void ApplyDataToSubject()
        {
            try
            {
                TaskName = SelectedSubject.title;
                StartDate = SelectedSubject.startDate;

                //đây là chỗ nhắc
                //if (!Reminders.Contains(SelectedSubject.NotifyTimeString))
                //    Reminders.Add(SelectedSubject.NotifyTimeString);
                string timeUnit = "";
                switch (SelectedSubject.notiUnit)
                {
                    case "MINUTE":
                        timeUnit = "phút";
                        break;
                    case "HOUR":
                        timeUnit = "tiếng";
                        break;
                    case "DAY":
                        timeUnit = "ngày";
                        break;
                    case "WEEK":
                        timeUnit = "tuần";
                        break;
                }
                TimeRemind = SelectedSubject.notiBeforeTime + " " + timeUnit;

                ColorTag = Color.FromHex(SelectedSubject.colorCode);

                //đây là giờ bắt đầu
                TimeSpan t = TimeSpan.FromSeconds(SelectedSubject.startTime);
                string answer = string.Format("{0:D2}h:{1:D2}m", t.Hours, t.Minutes);
                StartTimeX = answer.Split(':')[0];
                StartTimeY = answer.Split(':')[1];

                //số tiết trong ngày
                LessonPerDay = SelectedSubject.numOfLessonsPerDay;

                //ngày kết thúc hoặc số tiết
                if (SelectedSubject.numOfLessons != 0)
                {
                    HaveEndDate = false;
                    TotalLesson = SelectedSubject.numOfLessons;
                }
                else if (SelectedSubject.endDate != null)
                {
                    HaveEndDate = true;
                    EndDate = SelectedSubject.endDate;
                }

                //thứ ở đây
                string result = "";
                if (SelectedSubject.dayOfWeeks.Contains("Monday"))
                {
                    result += "Thứ hai,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Tuesday"))
                {
                    result += "Thứ ba,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Wednesday"))
                {
                    result += "Thứ tư,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Thursday"))
                {
                    result += "Thứ năm,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Friday"))
                {
                    result += "Thứ sáu,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Saturday"))
                {
                    result += "Thứ bảy,";
                }
                if (SelectedSubject.dayOfWeeks.Contains("Sunday"))
                {
                    result += "Chủ nhật,";
                }
                WeekDayLabel = result.Remove(result.Length - 1);
                Description = SelectedSubject.description;
            }
            catch (Exception)
            {
            }
            
        }

        private void ClearSubjectData()
        {
            try
            {
                TaskName = null;
                StartDate = DateTime.Now;

                //đây là chỗ nhắc
                InitData();
                TimeRemind = null;

                ColorTag = Color.White;

                //đây là giờ bắt đầu
                StartTimeX = null;
                StartTimeY = null;

                //số tiết
                LessonPerDay = null;

                //ngày kết thúc hoặc số tiết
                TotalLesson = null;
                EndDate = DateTime.Now;

                Description = null;
            }
            catch (Exception)
            {
            }
            
        }

        private bool IsValidData()
        {
            try
            {
                if (string.IsNullOrEmpty(taskName))
                {
                    App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng nhập tiêu đề", "Đóng");
                    return false;
                }
                if (ColorTag == Color.White)
                {
                    App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn màu hiển thị", "Đóng");
                    return false;
                }
                if (string.IsNullOrEmpty(TimeRemind))
                {
                    App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn thời gian nhắc nhở", "Đóng");
                    return false;
                }
                if (string.IsNullOrEmpty(StartTimeX) || string.IsNullOrEmpty(StartTimeY))
                {
                    App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn giờ bắt đầu", "Đóng");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void InitData()
        {
            try
            {
                WeekDayLabel = DateTime.Now.ToString("dddd");
                SelectedWeekDay = new List<string>
            {
                DateTime.Now.DayOfWeek.ToString()
            };
                Reminders = new ObservableCollection<string>
            {
                "5 phút",
                "10 phút",
                "15 phút",
                "30 phút",
                "1 tiếng",
                "1 ngày",
                "30 ngày"
            };
            }
            catch (Exception)
            {
            }
            
        }

        private int GetRemindTime()
        {
            string[] remind = TimeRemind.Split(' ');
            int time = int.Parse(remind[0]);
            if (remind[1] == "giờ")
            {
                time *= 60;
            }
            if (remind[1] == "ngày")
            {
                time *= 60 * 24;
            }
            if (remind[1] == "tuần")
            {
                time *= 60 * 24 * 7;
            }
            return time;
        }

        private string GetRemindTimeUnit()
        {
            string[] remind = TimeRemind.Split(' ');
            string time = "MINUTE";
            if (remind[1] == "tiếng")
            {
                time = "HOUR";
            }
            if (remind[1] == "ngày")
            {
                time = "DAY";
            }
            if (remind[1] == "tuần")
            {
                time = "WEEK";
            }
            return time;
        }

        private bool CheckTime(string minute)
        {
            if (int.Parse(minute) % 15 != 0)
            {
                App.Current.MainPage.DisplayAlert("Cảnh báo", "Số phút của giờ bắt đầu phải là 0, 15, 30, 45", "Đóng");
                return false;
            }
            else if ((int.Parse(minute) != 0) && int.Parse(minute) % 15 != 0)
            {
                App.Current.MainPage.DisplayAlert("Cảnh báo", "Số phút của giờ bắt đầu phải là 0, 15, 30, 45", "Đóng");
                return false;
            }
            return true;
        }
    }
}
