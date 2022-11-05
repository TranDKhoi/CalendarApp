﻿using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views.Manage;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

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

        private string remindLabel;
        public string RemindLabel
        {
            get { return remindLabel; }
            set { remindLabel = value; OnPropertyChanged(); }
        }


        public Command FirstLoadSubjectCM { get; set; }
        public Command ToEditSubjectCM { get; set; }
        public Command UpdateSubjectCM { get; set; }
        public Command DeleteSubjectCM { get; set; }
        public Command OpenCustomReminderPopupCM { get; set; }
        public Command OpenColorPickerCM { get; set; }
        public Command OpenRecurrencePopupCM { get; set; }
        public Command SearchSubjectCM { get; set; }

        public ManageViewModel()
        {

            FirstLoadSubjectCM = new Command(async () =>
            {
                SelectedSubject = null;
                ClearSubjectData();

                UserDialogs.Instance.ShowLoading();
                var res = await CourseService.ins.GetAllCourse();
                UserDialogs.Instance.HideLoading();

                if (res.isSuccess)
                {
                    ListSubject = new ObservableCollection<Subject>(res.data);

                    for (int i = 0; i < ListSubject.Count; i++)
                    {
                        TimeSpan t = TimeSpan.FromSeconds(ListSubject[i].startTime);
                        ListSubject[i].StartTimeUI = string.Format("{0:D1}:{1:D2}", t.Hours, t.Minutes);

                        TimeSpan t2 = TimeSpan.FromSeconds(ListSubject[i].startTime + 45 * 60 * ListSubject[i].numOfLessonsPerDay);
                        ListSubject[i].EndTimeUI = string.Format("{0:D1}:{1:D2}", t2.Hours, t2.Minutes);
                    }
                }
                else
                {
                   UserDialogs.Instance.Toast(res.message);
                }

            });
            ToEditSubjectCM = new Command(() =>
            {
                ApplyDataToSubject();
                App.Current.MainPage.Navigation.PushAsync(new EditSubjectScreen(this));
            });
            UpdateSubjectCM = new Command(async () =>
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
                if (HaveEndDate)
                {
                    if (!CheckTime(StartTimeY))
                    {
                        return;
                    }
                    // Gắn data trả về
                    Subject subject = new Subject
                    {
                        title = TaskName,
                        startDate = StartDate,
                        endDate = EndDate,
                        notiBeforeTime = GetRemindTime(),
                        startTime = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                        numOfLessonsPerDay = (int)LessonPerDay,
                        colorCode = ColorTag.ToHexRgbString(),
                        NotifyTimeString = TimeRemind,
                    };
                    if (!string.IsNullOrEmpty(Description))
                    {
                        subject.description = Description;
                    }
                    await App.Current.MainPage.DisplayAlert("Thành công", "Cập nhật môn học thành công", "Đóng");
                    await App.Current.MainPage.Navigation.PopAsync();
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
                    Subject subject = new Subject
                    {
                        title = TaskName,
                        startDate = StartDate,
                        notiBeforeTime = GetRemindTime(),
                        startTime = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                        numOfLessonsPerDay = (int)LessonPerDay,
                        numOfLessons = (int)TotalLesson,
                        colorCode = ColorTag.ToHexRgbString(),
                        NotifyTimeString = TimeRemind,
                    };
                    if (!string.IsNullOrEmpty(Description))
                    {
                        subject.description = Description;
                    }
                    await App.Current.MainPage.DisplayAlert("Thành công", "Cập nhật môn học thành công", "Đóng");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            });
            DeleteSubjectCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có chắc muốn xoá môn học này không?", "Có", "Không");
                if (res)
                    _ = App.Current.MainPage.Navigation.PopAsync();
            });
            SearchSubjectCM = new Command((p) =>
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
            });
            OpenCustomReminderPopupCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new CustomReminderPopup());
                if (res != null)
                {
                    if (!Reminders.Contains(res.ToString()))
                        Reminders.Add(res.ToString());
                    TimeRemind = res.ToString();
                }
            });
            OpenColorPickerCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new ColorPicker());
                if (res != null)
                {
                    ColorTag = (Color)res;
                }
            });
            OpenRecurrencePopupCM = new Command(async (p) =>
            {
                if (p != null)
                {
                    DateTime dateTime = (DateTime)p;
                    var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new RecurrencePopup(dateTime));
                    if (res != null)
                    {
                        Recurrence recurrence = res as Recurrence;
                        RemindLabel = recurrence.LabelDisplay;
                    }
                }

            });
        }

        private void ApplyDataToSubject()
        {
            TaskName = SelectedSubject.title;
            StartDate = SelectedSubject.startDate;

            //đây là chỗ nhắc
            if (!Reminders.Contains(SelectedSubject.NotifyTimeString))
                Reminders.Add(SelectedSubject.NotifyTimeString);
            TimeRemind = SelectedSubject.NotifyTimeString;

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

            Description = SelectedSubject.description;
        }

        private void ClearSubjectData()
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

        private bool IsValidData()
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

        private void InitData()
        {
            RemindLabel = "Không lặp lại";
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
