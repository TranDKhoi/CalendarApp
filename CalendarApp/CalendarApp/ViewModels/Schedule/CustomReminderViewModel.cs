using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Schedule
{
    public class CustomReminderViewModel : BaseViewModel
    {
        public ICommand DoneCM { get; set; }

        private string selectedCountDay;
        public string SelectedCountDay
        {
            get { return selectedCountDay; }
            set { selectedCountDay = value; OnPropertyChanged(); }
        }

        private string selectedTypeDate;
        public string SelectedTypeDate
        {
            get { return selectedTypeDate; }
            set { selectedTypeDate = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> customReminders;
        public ObservableCollection<string> CustomReminders
        {
            get { return customReminders; }
            set { customReminders = value; OnPropertyChanged(); }
        }

        private void InitData()
        {
            CustomReminders = new ObservableCollection<string>
            {
                "phút",
                "tiếng",
                "ngày",
                "tuần"
            };
        }
        
        public CustomReminderViewModel()
        {
            InitData();
            DoneCM = new Command((p) =>
            {
                try
                {
                    Popup popup = p as Popup;
                    if (!string.IsNullOrEmpty(SelectedCountDay) && !string.IsNullOrEmpty(SelectedTypeDate))
                    {
                        popup.Dismiss(SelectedCountDay + " " + SelectedTypeDate);
                    }
                }
                catch (Exception)
                {
                }
                
            });
        }
    }
}
