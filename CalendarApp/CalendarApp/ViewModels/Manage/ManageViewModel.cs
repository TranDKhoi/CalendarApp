using Acr.UserDialogs;
using CalendarApp.Models;
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

        private ObservableCollection<Todo> listTodo;
        public ObservableCollection<Todo> ListTodo
        {
            get { return listTodo; }
            set { listTodo = value; OnPropertyChanged(); }
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set { selectedSubject = value; OnPropertyChanged(); }
        }

        private Todo selectedTodo;
        public Todo SelectedTodo
        {
            get { return selectedTodo; }
            set { selectedTodo = value; OnPropertyChanged(); }
        }

        private string searchSubjectText;
        public string SearchSubjectText
        {
            get { return searchSubjectText; }
            set { searchSubjectText = value; OnPropertyChanged(); }
        }

        private string searchTodoText;
        public string SearchTodoText
        {
            get { return searchTodoText; }
            set { searchTodoText = value; OnPropertyChanged(); }
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


        public Command FirstLoadSubjectCM { get; set; }
        public Command ToEditSubjectCM { get; set; }
        public Command UpdateSubjectCM { get; set; }
        public Command DeleteSubjectCM { get; set; }
        public Command OpenCustomReminderPopupCM { get; set; }
        public Command OpenColorPickerCM { get; set; }
        public Command OpenRecurrencePopupCM { get; set; }
        public Command SearchSubjectCM { get; set; }
        public Command FirstLoadTaskCM { get; set; }
        public Command ToEditTodoCM { get; set; }
        public Command UpdateTodoCM { get; set; }
        public Command DeleteTodoCM { get; set; }
        public Command SearchTodoCM { get; set; }

        public ManageViewModel()
        {
            FirstLoadSubjectCM = new Command(() =>
            {
                SelectedSubject = null;
                ClearSubjectData();
                ListSubject = new ObservableCollection<Subject>();

                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 28000, Title = "Phát triển ứng dụng đa phương tiện trên thiết bị di động", Description = "Phòng B4.10, thày Phạm Nguyễn Trường An", NotifyTimeString = "5 phút", NumOfLessonsPerDay = 5, NumOfLessons = 10 });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 29000, Title = "Kinh tế chính trị Mác – Lênin", Description = "Phòng B6.06, thày Nguyễn Hữu Trinh", NotifyTimeString = "10 phút", NumOfLessonsPerDay = 5, NumOfLessons = 10 });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 22000, Title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động", Description = "Phòng B1.14, thày Võ Ngọc Tân", NotifyTimeString = "30 phút", NumOfLessonsPerDay = 5, NumOfLessons = 10 });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 21000, Title = "Công nghệ lập trình đa nền tảng cho ứng dụng di động (TH)", Description = "Phòng C111, thày Phạm Nhật Duy", NotifyTimeString = "1 tiếng", NumOfLessonsPerDay = 5, EndDate = new DateTime(2022, 3, 25) });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 20000, Title = "Phương pháp Phát triển phần mềm hướng đối tượng", Description = "Phòng B1.16, cô Nguyễn Hồng Thủy", NotifyTimeString = "1 ngày", NumOfLessonsPerDay = 5, EndDate = new DateTime(2022, 3, 25) });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 29000, Title = "Pháp luật đại cương", Description = "Phòng C205, cô Phạm Thị Thảo Xuyên", NotifyTimeString = "30 ngày", NumOfLessonsPerDay = 5, EndDate = new DateTime(2022, 3, 25) });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 25000, Title = "Công nghệ .NET", Description = "Phòng B6.02, cô Huỳnh Hồ Thị Mộng Trinh", NotifyTimeString = "5 phút", NumOfLessonsPerDay = 5, EndDate = new DateTime(2022, 3, 25) });
                ListSubject.Add(new Subject { ColorCode = "#49B583", StartDate = DateTime.Today, StartTimeInt = 24000, Title = "Quản lý dự án Phát triển Phần mềm", Description = "Phòng B1.18, cô Đỗ Thị Thanh Tuyền", NotifyTimeString = "5 phút", NumOfLessonsPerDay = 5, EndDate = new DateTime(2022, 3, 25) });

                for (int i = 0; i < ListSubject.Count; i++)
                {
                    TimeSpan t = TimeSpan.FromSeconds(ListSubject[i].StartTimeInt);
                    ListSubject[i].StartTime = string.Format("{0:D1}:{1:D2}", t.Hours, t.Minutes);

                    TimeSpan t2 = TimeSpan.FromSeconds(ListSubject[i].StartTimeInt + 45 * 60 * ListSubject[i].NumOfLessonsPerDay);
                    ListSubject[i].EndTime = string.Format("{0:D1}:{1:D2}", t2.Hours, t2.Minutes);
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
                        Title = TaskName,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        NotiBeforeTime = GetRemindTime(),
                        StartTimeInt = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                        NumOfLessonsPerDay = (int)LessonPerDay,
                        ColorCode = ColorTag.ToHexRgbString(),
                        NotifyTimeString = TimeRemind,
                    };
                    if (!string.IsNullOrEmpty(Description))
                    {
                        subject.Description = Description;
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
                        Title = TaskName,
                        StartDate = StartDate,
                        NotiBeforeTime = GetRemindTime(),
                        StartTimeInt = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                        NumOfLessonsPerDay = (int)LessonPerDay,
                        NumOfLessons = (int)TotalLesson,
                        ColorCode = ColorTag.ToHexRgbString(),
                        NotifyTimeString = TimeRemind,
                    };
                    if (!string.IsNullOrEmpty(Description))
                    {
                        subject.Description = Description;
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
                        if (ListSubject[i].Title.ToLower().Contains(SearchSubjectText.ToLower()))
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
            FirstLoadTaskCM = new Command(() =>
            {
                SelectedTodo = null;
                ClearTodoData();
                ListTodo = new ObservableCollection<Todo>();

                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Cuộc gặp với kiều bá dương", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "alo alo tile 2", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Ăn cơm với tổng thống", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Đây là tiêu đề", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Tập thể dục với crush", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Không biết ghi gì", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Cuộc gặp với kiều bá dương", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Cuộc gặp với kiều bá dương", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Cuộc gặp với kiều bá dương", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });
                ListTodo.Add(new Todo { StartTimeInt = 28000, EndTimeInt = 50000, Title = "Cuộc gặp với kiều bá dương", StartDate = DateTime.Today, ColorCode = "#49B583", Description = "Thằng KBD láo cá phải dạy cho nó một bài học", NotifyTimeString = "5 phút" });

                for (int i = 0; i < ListTodo.Count; i++)
                {
                    TimeSpan t = TimeSpan.FromSeconds(ListTodo[i].StartTimeInt);
                    ListTodo[i].StartTime = string.Format("{0:D1}:{1:D2}", t.Hours, t.Minutes);

                    TimeSpan t2 = TimeSpan.FromSeconds(ListTodo[i].EndTimeInt);
                    ListTodo[i].EndTime = string.Format("{0:D1}:{1:D2}", t2.Hours, t2.Minutes);
                }
            });
            ToEditTodoCM = new Command(() =>
            {
                ApplyDataToTodo();
                App.Current.MainPage.Navigation.PushAsync(new EditTodoScreen(this));
            });
            UpdateTodoCM = new Command(() =>
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
                // Gắn data trả về
                Todo todo = new Todo
                {
                    Title = TaskName,
                    StartDate = StartDate,
                    NotiBeforeTime = GetRemindTime(),
                    StartTimeInt = int.Parse(StartTimeX) * 3600 + int.Parse(StartTimeY) * 60,
                    EndTimeInt = int.Parse(EndTimeX) * 3600 + int.Parse(EndTimeY) * 60,
                    ColorCode = ColorTag.ToHexRgbString(),
                    NotifyTimeString = TimeRemind,
                };
                if (!string.IsNullOrEmpty(Description))
                {
                    todo.Description = Description;
                }
                App.Current.MainPage.DisplayAlert("Thành công", "Thêm thành công", "Đóng");

            });
            DeleteTodoCM = new Command(async () =>
            {
                var res = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có chắc muốn xoá công việc này không?", "Có", "Không");
                if (res)
                    _ = App.Current.MainPage.Navigation.PopAsync();
            });
            SearchTodoCM = new Command((p) =>
            {
                CollectionView cl = p as CollectionView;

                ObservableCollection<Todo> search = new ObservableCollection<Todo>();

                if (!string.IsNullOrEmpty(SearchTodoText))
                {

                    for (int i = 0; i < ListTodo.Count; i++)
                    {
                        if (ListTodo[i].Title.ToLower().Contains(SearchTodoText.ToLower()))
                        {
                            search.Add(ListTodo[i]);
                        }
                    }
                    cl.ItemsSource = search;
                }
                else
                {
                    cl.ItemsSource = ListTodo;
                }
            });
        }

        private void ApplyDataToTodo()
        {
            TaskName = SelectedTodo.Title;
            ColorTag = Color.FromHex(SelectedTodo.ColorCode);
            StartDate = SelectedTodo.StartDate;

            //đây là chỗ nhắc
            if (!Reminders.Contains(SelectedTodo.NotifyTimeString))
                Reminders.Add(SelectedTodo.NotifyTimeString);
            TimeRemind = SelectedTodo.NotifyTimeString;

            //start time and end time
            TimeSpan t1 = TimeSpan.FromSeconds(SelectedTodo.StartTimeInt);
            TimeSpan t2 = TimeSpan.FromSeconds(SelectedTodo.EndTimeInt);
            string answer1 = string.Format("{0:D2}h:{1:D2}m", t1.Hours, t1.Minutes);
            string answer2 = string.Format("{0:D2}h:{1:D2}m", t2.Hours, t2.Minutes);
            StartTimeX = answer1.Split(':')[0];
            StartTimeY = answer1.Split(':')[1];
            EndTimeX = answer2.Split(':')[0];
            EndTimeY = answer2.Split(':')[1];
        }

        private void ClearTodoData()
        {
            TaskName = null;
            ColorTag = Color.White;
            StartDate = DateTime.Now;

            //đây là chỗ nhắc
            InitData();
            TimeRemind = null;

            //start time and end time
            StartTimeX = null;
            StartTimeY = null;
            EndTimeX = null;
            EndTimeY = null;
        }

        private void ApplyDataToSubject()
        {
            TaskName = SelectedSubject.Title;
            StartDate = SelectedSubject.StartDate;

            //đây là chỗ nhắc
            if (!Reminders.Contains(SelectedSubject.NotifyTimeString))
                Reminders.Add(SelectedSubject.NotifyTimeString);
            TimeRemind = SelectedSubject.NotifyTimeString;

            ColorTag = Color.FromHex(SelectedSubject.ColorCode);

            //đây là giờ bắt đầu
            TimeSpan t = TimeSpan.FromSeconds(SelectedSubject.StartTimeInt);
            string answer = string.Format("{0:D2}h:{1:D2}m", t.Hours, t.Minutes);
            StartTimeX = answer.Split(':')[0];
            StartTimeY = answer.Split(':')[1];

            //số tiết trong ngày
            LessonPerDay = SelectedSubject.NumOfLessonsPerDay;

            //ngày kết thúc hoặc số tiết
            if (SelectedSubject.NumOfLessons != 0)
            {
                HaveEndDate = false;
                TotalLesson = SelectedSubject.NumOfLessons;
            }
            else if (SelectedSubject.EndDate != null)
            {
                HaveEndDate = true;
                EndDate = SelectedSubject.EndDate;
            }

            Description = SelectedSubject.Description;
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
