using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views.Authen;
using CalendarApp.Views.BottomBarCustom;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Authen
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        public Command LoginCM { get; set; }
        public Command ToForgotScreenCM { get; set; }
        public Command ToSignupScreenCM { get; set; }
        public LoginViewModel()
        {
            LoginCM = new Command(async () =>
            {
                if (isValidData())
                {
                    UserDialogs.Instance.ShowLoading();
                    ApiResponse<User> res = await AuthService.ins.Login(Email, Password);
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        SharedPreferenceService.ins.SetUserToken(res.token);
                        App.Current.MainPage = new NavigationPage(new BottomBarCustom());
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
                else
                {
                    UserDialogs.Instance.Toast("Vui lòng điền đầy đủ thông tin");
                }
            });
            ToForgotScreenCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new ForgotPassScreen());
            });
            ToSignupScreenCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new SignupScreen());
            });
        }

        private bool isValidData()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
    }
}
