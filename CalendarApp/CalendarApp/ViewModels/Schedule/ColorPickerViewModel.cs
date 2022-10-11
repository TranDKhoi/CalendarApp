using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Schedule
{
    public class ColorPickerViewModel : BaseViewModel
    {
        public ICommand SelectYellowCM { get; set; }
        public ICommand SelectRedCM { get; set; }
        public ICommand SelectPurpleCM { get; set; }
        public ICommand SelectBlueCM { get; set; }
        public ICommand SelectGreenCM { get; set; }
        public ICommand SelectPinkCM { get; set; }
        public ICommand DoneCM { get; set; }

        private bool isYellow;
        public bool IsYellow
        {
            get { return isYellow; }
            set { isYellow = value; OnPropertyChanged(); }
        }

        private bool isRed;
        public bool IsRed
        {
            get { return isRed; }
            set { isRed = value; OnPropertyChanged(); }
        }

        private bool isPurple;
        public bool IsPurple
        {
            get { return isPurple; }
            set { isPurple = value; OnPropertyChanged(); }
        }

        private bool isBlue;
        public bool IsBlue
        {
            get { return isBlue; }
            set { isBlue = value; OnPropertyChanged(); }
        }

        private bool isGreen;
        public bool IsGreen
        {
            get { return isGreen; }
            set { isGreen = value; OnPropertyChanged(); }
        }

        private bool isPink;
        public bool IsPink
        {
            get { return isPink; }
            set { isPink = value; OnPropertyChanged(); }
        }

        public ColorPickerViewModel()
        {
            IsYellow = false;
            IsRed = false;
            IsPurple = false;
            IsBlue = false;
            IsGreen = false;
            IsPink = false;
            SelectYellowCM = new Command(() =>
            {
                IsYellow = true;
                IsRed = false;
                IsPurple = false;
                IsBlue = false;
                IsGreen = false;
                IsPink = false;
            });
            SelectRedCM = new Command(() =>
            {
                IsYellow = false;
                IsRed = true;
                IsPurple = false;
                IsBlue = false;
                IsGreen = false;
                IsPink = false;
            });
            SelectPurpleCM = new Command(() =>
            {
                IsYellow = false;
                IsRed = false;
                IsPurple = true;
                IsBlue = false;
                IsGreen = false;
                IsPink = false;
            });
            SelectBlueCM = new Command(() =>
            {
                IsYellow = false;
                IsRed = false;
                IsPurple = false;
                IsBlue = true;
                IsGreen = false;
                IsPink = false;
            });
            SelectGreenCM = new Command(() =>
            {
                IsYellow = false;
                IsRed = false;
                IsPurple = false;
                IsBlue = false;
                IsGreen = true;
                IsPink = false;
            });
            SelectPinkCM = new Command(() =>
            {
                IsYellow = false;
                IsRed = false;
                IsPurple = false;
                IsBlue = false;
                IsGreen = false;
                IsPink = true;
            });
            DoneCM = new Command((p) =>
            {
                Popup popup = p as Popup;
                if (isYellow || isRed || isPurple || isBlue || isGreen || isPink)
                {
                    if (isYellow)
                    {
                        popup.Dismiss(Color.FromHex("#FFBD69"));
                    }
                    if (isRed)
                    {
                        popup.Dismiss(Color.FromHex("#FF4171"));
                    }
                    if (isPurple)
                    {
                        popup.Dismiss(Color.FromHex("#7F86FF"));
                    }
                    if (isBlue)
                    {
                        popup.Dismiss(Color.FromHex("#5b92e0"));
                    }
                    if (isGreen)
                    {
                        popup.Dismiss(Color.FromHex("#49B583"));
                    }
                    if (isPink)
                    {
                        popup.Dismiss(Color.FromHex("#e95dc0"));
                    }
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Cảnh báo", "Chọn màu bạn muốn đổi", "Đóng");
                }
                
            });
        }

    }
}
