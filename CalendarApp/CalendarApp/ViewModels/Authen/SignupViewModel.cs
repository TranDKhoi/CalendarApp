using Acr.UserDialogs;
using CalendarApp.Services;
using CalendarApp.Views.Authen;
using CalendarApp.Views.BottomBarCustom;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Authen
{
    public class SignupViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string pass;
        public string Pass
        {
            get { return pass; }
            set { pass = value; OnPropertyChanged(); }
        }

        private string rePass;
        public string RePass
        {
            get { return rePass; }
            set { rePass = value; OnPropertyChanged(); }
        }

        private string verifyCode;
        public string VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; OnPropertyChanged(); }
        }


        public Command ToVerifyScreenCM { get; set; }
        public Command PopCM { get; set; }
        public Command VerifyCodeCM { get; set; }

        public SignupViewModel()
        {
            ToVerifyScreenCM = new Command(async () =>
            {
                if (!isValidData())
                {
                    UserDialogs.Instance.Toast("Vui lòng điền đủ thông tin");
                    return;
                }
                if (RePass != Pass)
                {
                    UserDialogs.Instance.Toast("Mật khẩu không khớp");
                    return;
                }
                if (RePass.Length < 6)
                {
                    UserDialogs.Instance.Toast("Mật khẩu tối thiểu 6 ký tự");
                    return;
                }

                UserDialogs.Instance.ShowLoading();
                var res = await AuthService.ins.Register(Email, RePass);
                UserDialogs.Instance.HideLoading();

                if (res.isSuccess)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VerifySignup(this));
                }
                else
                {
                    UserDialogs.Instance.Toast(res.message);
                }


            });
            PopCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
            });
            VerifyCodeCM = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(VerifyCode))
                {
                    UserDialogs.Instance.ShowLoading();
                    var res = await AuthService.ins.VerifyAccount(Email, VerifyCode);
                    UserDialogs.Instance.HideLoading();

                    if (res.isSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Tạo tài khoản thành công", "Đóng");
                        App.Current.MainPage = new NavigationPage(new BottomBarCustom());
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(res.message);
                    }
                }
            });
        }

        private bool isValidData()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Pass) && !string.IsNullOrEmpty(RePass);
        }
    }
}
