using CalendarApp.Models;
using CalendarApp.ViewModels.Converter;
using CalendarApp.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Schedule
{
    public class AddTaskViewModel : BaseViewModel
    {
        #region Môn học

        private int? lessonPerDay;
        public int? LessonPerDay
        {
            get { return lessonPerDay; }
            set { lessonPerDay = value; OnPropertyChanged(); }
        }

        private int? totalLesson;
        public int? TotalLesson
        {
            get { return totalLesson; }
            set { totalLesson = value; OnPropertyChanged(); }
        }

        private bool haveEndDate;
        public bool HaveEndDate
        {
            get { return haveEndDate; }
            set { haveEndDate = value; OnPropertyChanged(); }
        }

        private string weekDayLabel;
        public string WeekDayLabel
        {
            get { return weekDayLabel; }
            set { weekDayLabel = value; OnPropertyChanged(); }
        }

        // data tra ve cho thg dat
        private List<string> selectedWeekDay;
        public List<string> SelectedWeekDay
        {
            get { return selectedWeekDay; }
            set { selectedWeekDay = value; }
        }


        #endregion

        #region Việc cần làm

        private Color colorTag;
        public Color ColorTag
        {
            get { return colorTag; }
            set { colorTag = value; OnPropertyChanged(); }
        }

        private string endTimeX;
        public string EndTimeX
        {
            get { return endTimeX; }
            set { endTimeX = value; OnPropertyChanged(); }
        }

        private string endTimeY;
        public string EndTimeY
        {
            get { return endTimeY; }
            set { endTimeY = value; OnPropertyChanged(); }
        }

        private string remindLabel;
        public string RemindLabel
        {
            get { return remindLabel; }
            set { remindLabel = value; OnPropertyChanged(); }
        }

        #endregion

        private ObservableCollection<string> reminders;
        public ObservableCollection<string> Reminders
        {
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

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

        private string timeRemind;
        public string TimeRemind
        {
            get { return timeRemind; }
            set { timeRemind = value; OnPropertyChanged(); }
        }

        private int timeRemindIndex;
        public int TimeRemindIndex
        {
            get { return timeRemindIndex; }
            set { timeRemindIndex = value; OnPropertyChanged(); }
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

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private Recurrence selectedRecurrence;
        public Recurrence SelectedRecurrence
        {
            get { return selectedRecurrence; }
            set { selectedRecurrence = value; OnPropertyChanged(); }
        }

        private void InitData()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            LessonPerDay = null;
            HaveEndDate = true;
            TotalLesson = null;
            ColorTag = Color.White;
            RemindLabel = "Không lặp lại";
            WeekDayLabel = DateTime.Now.ToString("dddd");
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
            SelectedWeekDay = new List<string>
            {
                DateTime.Now.DayOfWeek.ToString()
            };
        }

        private int GetRemindTime()
        {
            string[] remind = TimeRemind.Split(' ');
            int time = int.Parse(remind[0]);
            if (remind[1] == "tiếng")
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
            string unit = "";
            if (remind[1] == "phút")
            {
                unit = "MINUTE";
            }
            if (remind[1] == "tiếng")
            {
                unit = "HOUR";
            }
            if (remind[1] == "ngày")
            {
                unit = "DAY";
            }
            if (remind[1] == "tuần")
            {
                unit = "WEEK";
            }
            return unit;
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

        #region Commands
        public ICommand OpenCustomReminderPopupCM { get; set; }
        public ICommand OpenColorPickerCM { get; set; }
        public ICommand OpenRecurrencePopupCM { get; set; }
        public ICommand OpenSelectDayOfWeekPopupCM { get; set; }
        public ICommand AddSubjectCM { get; set; }
        public ICommand AddTodoCM { get; set; }
        public ICommand DoneCM { get; set; }
        #endregion

        public AddTaskViewModel()
        {
            InitData();

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
                        SelectedRecurrence = res as Recurrence;
                        RemindLabel = SelectedRecurrence.LabelDisplay;
                    }
                }

            });

            OpenSelectDayOfWeekPopupCM = new Command(async (p) =>
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
                                temp += ",";
                            }
                            tempList.Add(selectedWeekDay[i].DetailEng);
                        }
                        WeekDayLabel = temp;
                        SelectedWeekDay = tempList;
                    }
                }

            });

            AddSubjectCM = new Command((p) =>
            {
                if (p != null)
                {
                    if (!IsValidData())
                    {
                        return;
                    }
                    if (LessonPerDay == null)
                    {
                        App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng nhập số tiết", "Đóng");
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
                            //prop code here
                            //===
                            description = Description ?? "",
                            startTime = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                            dayOfWeeks = SelectedWeekDay,
                            numOfLessonsPerDay = (int)LessonPerDay,
                            startDate = StartDate,
                            endDate = EndDate,
                            notiBeforeTime = GetRemindTime(),
                            colorCode = ColorTag.ToHexRgbString(),
                            notiUnit = GetRemindTimeUnit(),
                        };
                        Popup popup = p as Popup;
                        popup.Dismiss(subject);
                    }
                    else
                    {
                        if (TotalLesson == null)
                        {
                            App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng nhập tổng số tiết", "Đóng");
                            return;
                        }
                        if (TotalLesson < LessonPerDay)
                        {
                            App.Current.MainPage.DisplayAlert("Cảnh báo", "Tổng số tiết không được nhỏ hơn số tiết 1 buổi", "Đóng");
                            return;
                        }
                        if (TotalLesson % LessonPerDay != 0)
                        {
                            App.Current.MainPage.DisplayAlert("Cảnh báo", "Tổng số tiết không chia hết cho số tiết một ngày", "Đóng");
                            return;
                        }
                        // Gắn data trả về
                        Subject subject = new Subject
                        {
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
                        Popup popup = p as Popup;
                        popup.Dismiss(subject);
                    }
                }
            });

            AddTodoCM = new Command((p) =>
            {
                if (p != null)
                {
                    if (!IsValidData())
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(EndTimeX) || string.IsNullOrEmpty(EndTimeY))
                    {
                        App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn giờ kết thúc", "Đóng");
                        return;
                    }
                    if (!CheckTime(EndTimeY))
                    {
                        return;
                    }
                    if (int.Parse(StartTimeX) > int.Parse(EndTimeX))
                    {
                        App.Current.MainPage.DisplayAlert("Cảnh báo", "Giờ bắt đầu phải lớn hơn giờ kết thúc", "Đóng");
                        return;
                    }
                    if (int.Parse(StartTimeX) == int.Parse(EndTimeX))
                    {
                        if (int.Parse(StartTimeY) >= int.Parse(EndTimeY))
                        {
                            App.Current.MainPage.DisplayAlert("Cảnh báo", "Giờ bắt đầu phải lớn hơn giờ kết thúc", "Đóng");
                            return;
                        }

                    }
                    // Gắn data trả về
                    Event todo = new Event
                    {
                        title = TaskName,
                        description = Description ?? "",
                        startTime = new DateTime(year: DateTime.Now.Year, day: DateTime.Now.Day, month: DateTime.Now.Month, hour: int.Parse(StartTimeX), minute: int.Parse(StartTimeY), second: DateTime.Now.Second),
                        endTime = new DateTime(year: DateTime.Now.Year, day: DateTime.Now.Day, month: DateTime.Now.Month, hour: int.Parse(EndTimeX), minute: int.Parse(EndTimeY), second: DateTime.Now.Second),
                        colorCode = ColorTag.ToHexRgbString(),
                        notiBeforeTime = GetRemindTime(),
                        notiUnit = GetRemindTimeUnit(),
                        recurringInterval = SelectedRecurrence.QuantityRecurrence,
                        recurringUnit = SelectedRecurrence.GetTypeStartRecurrence().ToString(),
                    };
                    if (SelectedRecurrence.GetTypeStartRecurrence() == TypeStartRecurrence.WEEK)
                    {
                        todo.recurringDetails = SelectedRecurrence.WeekDay;
                    }
                    if (SelectedRecurrence.GetTypeEndRecurrence() == TypeEndRecurrence.Date)
                    {
                        todo.recurringEnd = SelectedRecurrence.EndDate;
                    }
                    Popup popup = p as Popup;
                    popup.Dismiss(todo);
                }
            });
        }

    }
}
