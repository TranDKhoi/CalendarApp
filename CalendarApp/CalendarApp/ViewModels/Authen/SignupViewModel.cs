using Acr.UserDialogs;
using CalendarApp.Views.Authen;
using System;
using System.Collections.Generic;
using System.Text;
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

        private int verifyCode;
        public int VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; OnPropertyChanged(); }
        }


        public Command ToVerifyScreenCM { get; set; }
        public Command PopCM { get; set; }
        public Command VerifyCodeCM { get; set; }

        public SignupViewModel()
        {
            ToVerifyScreenCM = new Command(() =>
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

                Application.Current.MainPage.Navigation.PushAsync(new VerifySignup(this));

            });
            PopCM = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
            });
            VerifyCodeCM = new Command(() =>
            {
                //
            });
        }

        private bool isValidData()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Pass) && !string.IsNullOrEmpty(RePass);
        }
    }
}
