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
    public class RecurrenceViewModel : BaseViewModel
    {
        public ICommand SelectDayCM { get; set; }
        public ICommand DoneCM { get; set; }

        #region UI properties

        private bool loopLabel;
        public bool LoopLabel
        {
            get { return loopLabel; }
            set { loopLabel = value; OnPropertyChanged(); }
        }

        private bool customWeek;
        public bool CustomWeek
        {
            get { return customWeek; }
            set { customWeek = value; OnPropertyChanged(); }
        }

        private bool customMonth;
        public bool CustomMonth
        {
            get { return customMonth; }
            set { customMonth = value; OnPropertyChanged(); }
        }

        private bool neverEnd;
        public bool NeverEnd
        {
            get { return neverEnd; }
            set { neverEnd = value; OnPropertyChanged(); }
        }

        private bool endAtDate;
        public bool EndAtDate
        {
            get { return endAtDate; }
            set { endAtDate = value; OnPropertyChanged(); }
        }

        private bool endCount;
        public bool EndCount
        {
            get { return endCount; }
            set { endCount = value; OnPropertyChanged(); }
        }

        #endregion

        private ObservableCollection<string> typeRecurrence;
        public ObservableCollection<string> TypeRecurrence
        {
            get { return typeRecurrence; }
            set { typeRecurrence = value; OnPropertyChanged(); }
        }

        private string selectedTypeRecurrence;
        public string SelectedTypeRecurrence
        {
            get { return selectedTypeRecurrence; }
            set
            {
                selectedTypeRecurrence = value;
                if (selectedTypeRecurrence == "ngày")
                {
                    CustomMonth = false;
                    CustomWeek = false;
                    LoopLabel = false;
                }
                if (selectedTypeRecurrence == "tuần")
                {
                    CustomWeek = true;
                    CustomMonth = false;
                    LoopLabel = true;
                }
                if (selectedTypeRecurrence == "tháng")
                {
                    CustomMonth = true;
                    CustomWeek = false;
                    LoopLabel = true;
                    MonthLRecurrenceLabel = $"Hàng tháng vào ngày {Today.Day}";
                }
                OnPropertyChanged();
            }
        }

        private string monthLRecurrenceLabel;
        public string MonthLRecurrenceLabel
        {
            get { return monthLRecurrenceLabel; }
            set { monthLRecurrenceLabel = value; OnPropertyChanged(); }
        }

        private int recurreneQuantity;
        public int RecurreneQuantity
        {
            get { return recurreneQuantity; }
            set { recurreneQuantity = value; OnPropertyChanged(); }
        }

        private int endRecurreneQuantity;
        public int EndRecurreneQuantity
        {
            get { return endRecurreneQuantity; }
            set { endRecurreneQuantity = value; OnPropertyChanged(); }
        }

        private DateTime today;
        public DateTime Today
        {
            get { return today; }
            set
            {
                today = value;
                EndDate = Today.AddDays(1);
                OnPropertyChanged();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged(); }
        }

        private int typeRecurrenceIndex;
        public int TypeRecurrenceIndex
        {
            get { return typeRecurrenceIndex; }
            set { typeRecurrenceIndex = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DayTitle> days;
        public ObservableCollection<DayTitle> Days
        {
            get { return days; }
            set { days = value; OnPropertyChanged(); }
        }

        private void InitData()
        {
            try
            {
                NeverEnd = true;
                EndAtDate = false;
                EndCount = false;
                LoopLabel = false;
                RecurreneQuantity = 1;
                EndRecurreneQuantity = 1;
                TypeRecurrenceIndex = 0;

                Days = new ObservableCollection<DayTitle>
            {
                new DayTitle { Title = "T2", IsSelected = false ,DetailEng = "Monday",},
                new DayTitle { Title = "T3", IsSelected = false,DetailEng ="Tuesday", },
                new DayTitle { Title = "T4", IsSelected = false,DetailEng = "Wednesday" },
                new DayTitle { Title = "T5", IsSelected = false, DetailEng = "Thursday" },
                new DayTitle { Title = "T6", IsSelected = false ,DetailEng = "Friday"},
                new DayTitle { Title = "T7", IsSelected = false,DetailEng = "Saturday" },
                new DayTitle { Title = "Cn", IsSelected = false,DetailEng = "Sunday" }
            };

                TypeRecurrence = new ObservableCollection<string>
            {
                "ngày",
                "tuần",
                "tháng"
            };
            }
            catch (Exception)
            {
            }
            
        }

        public RecurrenceViewModel()
        {
            InitData();

            SelectDayCM = new Command((p) =>
            {
                try
                {
                    if (p != null)
                    {
                        DayTitle dayTitle = p as DayTitle;
                        dayTitle.IsSelected = !dayTitle.IsSelected;
                    }
                }
                catch (Exception)
                {
                }
                
            });

            DoneCM = new Command((p) =>
            {
                try
                {
                    if (p != null)
                    {
                        Recurrence recurrence = new Recurrence();
                        recurrence.ChoosenWeeksDay = new List<string>();
                        recurrence.WeekDay = new List<string>();
                        if (RecurreneQuantity <= 0)
                        {
                            App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập số lần lặp lớn hơn 0", "Đóng");
                            return;
                        }
                        if (EndCount)
                        {
                            if (EndRecurreneQuantity <= 0)
                            {
                                App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập số lần xuất hiện lớn hơn 0", "Đóng");
                                return;
                            }
                            recurrence.SetTypeEndRecurrence(TypeEndRecurrence.Count);
                            recurrence.EndCount = EndRecurreneQuantity;
                        }
                        if (NeverEnd)
                        {
                            recurrence.SetTypeEndRecurrence(TypeEndRecurrence.Infinity);
                        }
                        if (EndAtDate)
                        {
                            if (EndDate <= Today)
                            {
                                App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn ngày kết thúc lớn hơn ngày bắt đầu", "Đóng");
                                return;
                            }
                            recurrence.SetTypeEndRecurrence(TypeEndRecurrence.Date);
                            recurrence.EndDate = EndDate;
                        }
                        recurrence.QuantityRecurrence = RecurreneQuantity;
                        if (SelectedTypeRecurrence == "ngày")
                        {
                            recurrence.SetTypeStartRecurrence(TypeStartRecurrence.DAY);
                            if (NeverEnd)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} ngày một lần";
                            }
                            if (EndAtDate)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} ngày một lần, cho tới {EndDate.ToString("dd/MM/yyyy")}";
                            }
                            if (EndCount)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} ngày một lần, {EndRecurreneQuantity} lần";
                            }
                        }
                        if (SelectedTypeRecurrence == "tuần")
                        {
                            recurrence.SetTypeStartRecurrence(TypeStartRecurrence.WEEK);
                            string weeksDay = "";
                            for (int i = 0; i < Days.Count; i++)
                            {
                                if (Days[i].IsSelected)
                                {
                                    recurrence.ChoosenWeeksDay.Add(Days[i].Title);
                                    recurrence.WeekDay.Add(Days[i].DetailEng);
                                    weeksDay += Days[i].Title + ", ";
                                }
                            }
                            weeksDay = weeksDay.Remove(weeksDay.Length - 1);
                            weeksDay = weeksDay.Remove(weeksDay.Length - 1);
                            if (NeverEnd)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tuần một lần vào {weeksDay}";
                            }
                            if (EndAtDate)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tuần một lần vào {weeksDay}, cho tới {EndDate.ToString("dd/MM/yyyy")}";
                            }
                            if (EndCount)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tuần một lần vào {weeksDay}, {EndRecurreneQuantity} lần";
                            }
                        }
                        if (SelectedTypeRecurrence == "tháng")
                        {
                            recurrence.SetTypeStartRecurrence(TypeStartRecurrence.MONTH);
                            if (NeverEnd)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tháng một lần vào ngày {Today.Day}";
                            }
                            if (EndAtDate)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tháng một lần vào ngày {Today.Day}, cho tới {EndDate.ToString("dd/MM/yyyy")}";
                            }
                            if (EndCount)
                            {
                                recurrence.LabelDisplay = $"{RecurreneQuantity} tháng một lần vào ngày {Today.Day}, {EndRecurreneQuantity} lần";
                            }
                        }
                        RecurrencePopup popup = p as RecurrencePopup;
                        popup.Dismiss(recurrence);
                    }
                }
                catch (Exception)
                {
                }
                
            });
        }
    }
}
