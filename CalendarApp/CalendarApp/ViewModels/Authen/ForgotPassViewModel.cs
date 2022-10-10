using Acr.UserDialogs;
using CalendarApp.Views.Authen;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Authen
{
    public class ForgotPassViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private int verifyCode;
        public int VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; OnPropertyChanged(); }
        }


        public Command PopCM { get; set; }
        public Command ToVerifyScreenCM { get; set; }
        public Command VerifyCodeCM { get; set; }

        public ForgotPassViewModel()
        {
            PopCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
            });
            ToVerifyScreenCM = new Command(() =>
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    Application.Current.MainPage.Navigation.PushAsync(new VerifyForgot(this));
                }
                else
                {
                    UserDialogs.Instance.Toast("Vui lòng điền email để khôi phục");
                }
            });
            VerifyCodeCM = new Command(() =>
            {
                //
            });
        }
    }
}
