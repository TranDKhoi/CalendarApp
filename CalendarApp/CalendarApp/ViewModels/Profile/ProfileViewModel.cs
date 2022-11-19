using CalendarApp.Models.Profile;
using CalendarApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using CalendarApp.Services;
using CalendarApp.Views.Authen;

namespace CalendarApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileModel ProfileModel { get; } = new ProfileModel();
        public Command EditProfileScreenNavCommand { get; set; }
        public Command LogoutCM { get; set; }
        public ProfileViewModel()
        {

            ProfileModel.NameProfile = "Phap";
            ProfileModel.StatusProfile = "Mai mai en nhau";
            ProfileModel.UrlBackground = "avatar.jpg";
            ProfileModel.UrlAvatar = "anh.jpg";
            EditProfileScreenNavCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new EditProfileScreen());
            });
            LogoutCM = new Command(() =>
            {
                SharedPreferenceService.ins.ClearUserLogin();
                SharedPreferenceService.ins.ClearUserToken();
                Application.Current.MainPage = new NavigationPage(new LoginScreen());
                App.Current.MainPage.Navigation.PopToRootAsync();
            });

        }


    }
}