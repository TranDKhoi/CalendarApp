using Acr.UserDialogs;
using CalendarApp.Services;
using CalendarApp.Views.Authen;
using CalendarApp.Views.BottomBarCustom;
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

        private string verifyCode;
        public string VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string rePassword;
        public string RePassword
        {
            get { return rePassword; }
            set { rePassword = value; OnPropertyChanged(); }
        }



        private string tempToken;

        public Command PopCM { get; set; }
        public Command ToVerifyScreenCM { get; set; }
        public Command VerifyCodeCM { get; set; }
        public Command ResetPassCM { get; set; }

        public ForgotPassViewModel()
        {
            PopCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
            });
            ToVerifyScreenCM = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    UserDialogs.Instance.ShowLoading();
                    var res = await AuthService.ins.ForgotPassword(Email);
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        _ = Application.Current.MainPage.Navigation.PushAsync(new VerifyForgot(this));
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
                else
                {
                    UserDialogs.Instance.Toast("Vui lòng điền email để khôi phục");
                }
            });
            VerifyCodeCM = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(VerifyCode))
                {
                    UserDialogs.Instance.ShowLoading();
                    var res = await AuthService.ins.VerifyRePass(Email, VerifyCode);
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        tempToken = res.data;
                        App.Current.MainPage = new NavigationPage(new ResetPassScreen(this));
                        _ = App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
            });
            ResetPassCM = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RePassword))
                {
                    UserDialogs.Instance.Toast("Vui lòng nhập đủ thông tin");
                    return;
                }
                if (Password != RePassword)
                {
                    UserDialogs.Instance.Toast("Mật khẩu không khớp");
                    return;
                }
                if (Password.Length < 6)
                {
                    UserDialogs.Instance.Toast("Mật khẩu tối thiểu 6 ký tự");
                    return;
                }

                UserDialogs.Instance.ShowLoading();
                var res = await AuthService.ins.ResetPassword(tempToken, Password);
                UserDialogs.Instance.HideLoading();

                if (res.isSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Khôi phục mật khẩu thành công", "Đóng");
                    App.Current.MainPage = new NavigationPage(new LoginScreen());
                }
                else
                {
                    UserDialogs.Instance.Toast(res.message);
                }

            });
        }
    }
}
