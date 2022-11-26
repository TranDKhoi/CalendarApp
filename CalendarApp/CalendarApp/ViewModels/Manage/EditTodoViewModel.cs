﻿using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Utils;
using CalendarApp.Views.Manage;
using CalendarApp.Views.Schedule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Manage
{
    public class EditTodoViewModel : BaseViewModel
    {
        private Event currentTodo;
        public Event CurrentTodo
        {
            get { return currentTodo; }
            set
            {
                currentTodo = value;
                OnPropertyChanged();
            }
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

        private Color colorTag;
        public Color ColorTag
        {
            get { return colorTag; }
            set { colorTag = value; OnPropertyChanged(); }
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

        private Recurrence selectedRecurrence;
        public Recurrence SelectedRecurrence
        {
            get { return selectedRecurrence; }
            set { selectedRecurrence = value; OnPropertyChanged(); }
        }

        private int? oldInterval;

        public Command ApplyDataCM { get; set; }
        public Command UpdateTodoCM { get; set; }
        public Command OpenCustomReminderPopupCM { get; set; }
        public Command OpenRecurrencePopupCM { get; set; }
        public Command OpenColorPickerCM { get; set; }
        public Command DeleteTodoCM { get; set; }

        public EditTodoViewModel()
        {
            InitData();
            ApplyDataCM = new Command(() =>
            {
                ApplyDataToTodo();
            });
            UpdateTodoCM = new Command(async () =>
            {
                if (!IsValidData())
                {
                    return;
                }
                if (string.IsNullOrEmpty(EndTimeX) || string.IsNullOrEmpty(EndTimeY))
                {
                    _ = App.Current.MainPage.DisplayAlert("Cảnh báo", "Vui lòng chọn giờ kết thúc", "Đóng");
                    return;
                }
                if (!CheckTime(EndTimeY))
                {
                    return;
                }

                string SelectedTargetType = "";

                var resDia = await App.Current.MainPage.Navigation.ShowPopupAsync(new CustomSaveAsDialog());
                if (resDia != null)
                {
                    SelectedTargetType = resDia as string;
                }

                //Gắn data trả về
                Event todo = new Event
                {
                    id = CurrentTodo.id,
                    title = TaskName,
                    description = Description ?? "",
                    startTime = new DateTime(year: StartDate.Year, day: StartDate.Day, month: StartDate.Month, hour: int.Parse(StartTimeX), minute: int.Parse(StartTimeY), second: DateTime.Now.Second),
                    endTime = new DateTime(year: StartDate.Year, day: StartDate.Day, month: StartDate.Month, hour: int.Parse(EndTimeX), minute: int.Parse(EndTimeY), second: DateTime.Now.Second),
                    colorCode = ColorTag.ToHexRgbString(),
                    baseEventId = CurrentTodo.baseEventId,
                    cloneEventId = CurrentTodo.cloneEventId,
                    notiBeforeTime = GetRemindTime(),
                    notiUnit = GetRemindTimeUnit(),
                    beforeStartTime = CurrentTodo.startTime,
                    targetType = string.IsNullOrEmpty(SelectedTargetType) ? "THIS" : SelectedTargetType,
                };

                if (SelectedRecurrence != null)
                {
                    todo.recurringInterval = SelectedRecurrence.QuantityRecurrence;
                    todo.recurringUnit = SelectedRecurrence.GetTypeStartRecurrence().ToString();
                    if (SelectedRecurrence.GetTypeStartRecurrence() == TypeStartRecurrence.WEEK)
                    {
                        todo.recurringDetails = SelectedRecurrence.WeekDay;
                    }
                    if (SelectedRecurrence.GetTypeEndRecurrence() == TypeEndRecurrence.Date)
                    {
                        todo.recurringEnd = SelectedRecurrence.EndDate;
                    }
                }
                else
                {
                    todo.recurringInterval = CurrentTodo.recurringInterval;
                    todo.recurringUnit = CurrentTodo.recurringUnit;
                    todo.recurringDetails = CurrentTodo.recurringDetails;
                    todo.recurringEnd = CurrentTodo.recurringEnd;
                }

                try
                {
                    UserDialogs.Instance.ShowLoading();
                    var res = await EventService.ins.UpdateTask(todo);
                    UserDialogs.Instance.HideLoading();
                    if (res.isSuccess)
                    {
                        _ = App.Current.MainPage.DisplayAlert("Thành công", "Lưu thành công", "Đóng");
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
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
            OpenColorPickerCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(new ColorPicker());
                if (res != null)
                {
                    ColorTag = (Color)res;
                }
            });
            DeleteTodoCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có chắc muốn xoá công việc này không?", "Có", "Không");
                if (res)
                {
                    string SelectedTargetType = "";

                    var resDia = await App.Current.MainPage.Navigation.ShowPopupAsync(new CustomSaveAsDialog());
                    if (resDia != null)
                    {
                        SelectedTargetType = resDia as string;
                    }

                    Event deleteTodo = new Event
                    {
                        id = CurrentTodo.id,
                        baseEventId = CurrentTodo.baseEventId,
                        cloneEventId = CurrentTodo.cloneEventId,
                        startTime = CurrentTodo.startTime,
                        targetType = string.IsNullOrEmpty(SelectedTargetType) ? "THIS" : SelectedTargetType
                    };

                    UserDialogs.Instance.ShowLoading();
                    var result = await EventService.ins.DeleetTask(deleteTodo);
                    UserDialogs.Instance.HideLoading();

                    if (result.isSuccess)
                    {
                        UserDialogs.Instance.Toast("Xoá công việc thành công");
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(result.message);
                    }

                }
                _ = App.Current.MainPage.Navigation.PopAsync();
            });
        }

        private void ApplyDataToTodo()
        {
            TaskName = CurrentTodo.title;
            StartDate = CurrentTodo.startTime;
            ColorTag = Color.FromHex(CurrentTodo.colorCode);

            ////đây là chỗ nhắc
            string timeUnit = "";
            switch (CurrentTodo.notiUnit)
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
            TimeRemind = CurrentTodo.notiBeforeTime + " " + timeUnit;

            //start time and end time
            StartTimeX = CurrentTodo.startTime.ToString("HH");
            StartTimeY = CurrentTodo.startTime.ToString("mm");
            EndTimeX = CurrentTodo.endTime.ToString("HH");
            EndTimeY = CurrentTodo.endTime.ToString("mm");
            Description = CurrentTodo.description;

            // recurrence label
            if (CurrentTodo.recurringUnit == "DAY")
            {
                if (CurrentTodo.recurringEnd == null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} ngày một lần";
                }
                if (CurrentTodo.recurringEnd != null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} ngày một lần, cho tới {CurrentTodo.recurringEnd?.ToString("dd/MM/yyyy")}";
                }
            }
            if (CurrentTodo.recurringUnit == "WEEK")
            {
                string weeksDay = "";
                Dictionary<string, string> week = new Dictionary<string, string>();
                week["Monday"] = "Thứ hai";
                week["Tuesday"] = "Thứ ba";
                week["Wednesday"] = "Thứ tư";
                week["Thursday"] = "Thứ năm";
                week["Friday"] = "Thứ sáu";
                week["Saturday "] = "Thứ bảy";
                week["Sunday "] = "Chủ nhật";
                for (int i = 0; i < CurrentTodo.recurringDetails.Count; i++)
                {
                    weeksDay += week[CurrentTodo.recurringDetails[i]];
                    if (i < CurrentTodo.recurringDetails.Count - 1)
                    {
                        weeksDay += ", ";
                    }
                }
                if (CurrentTodo.recurringEnd == null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} tuần một lần vào {weeksDay}";
                }
                if (CurrentTodo.recurringEnd != null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} tuần một lần vào {weeksDay}, cho tới {CurrentTodo.recurringEnd?.ToString("dd/MM/yyyy")}";
                }
            }
            if (CurrentTodo.recurringUnit == "MONTH")
            {
                if (CurrentTodo.recurringEnd == null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} tháng một lần vào ngày {CurrentTodo.startTime.Day}";
                }
                if (CurrentTodo.recurringEnd != null)
                {
                    RemindLabel = $"{CurrentTodo.recurringInterval} tháng một lần vào ngày {CurrentTodo.startTime.Day}, cho tới {CurrentTodo.recurringEnd?.ToString("dd/MM/yyyy")}";
                }
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
                App.Current.MainPage.DisplayAlert("Cảnh báo", "Số phút của giờ kết thúc phải là 0, 15, 30, 45", "Đóng");
                return false;
            }
            else if ((int.Parse(minute) != 0) && int.Parse(minute) % 15 != 0)
            {
                App.Current.MainPage.DisplayAlert("Cảnh báo", "Số phút của giờ kết thúc phải là 0, 15, 30, 45", "Đóng");
                return false;
            }
            return true;
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
    }
}
