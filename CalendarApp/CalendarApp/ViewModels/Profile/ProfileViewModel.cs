
using CalendarApp.Models.Profile;
using CalendarApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

using CalendarApp.Views.Authen;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.IO;
using CalendarApp.Services;

namespace CalendarApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        string nameProfile;
        public string NameProfile
        {
            get { return nameProfile; }
            set { nameProfile = value; OnPropertyChanged(nameof(NameProfile)); }
        }
        string statusProfile;
        public string StatusProfile
        {
            get { return statusProfile; }
            set { statusProfile = value; OnPropertyChanged(nameof(StatusProfile)); }
        }
        ImageSource urlBackground;
        public ImageSource UrlBackground
        {
            get { return urlBackground; }
            set { urlBackground = value; OnPropertyChanged(nameof(UrlBackground)); }
        }
        ImageSource urlAvatar;
        public ImageSource UrlAvatar
        {
            get { return urlAvatar; }
            set { urlAvatar = value; OnPropertyChanged(); }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        string nameFull;
        public string NameFull
        {
            get { return nameFull; }
            set { nameFull = value; OnPropertyChanged(); }
        }


        public ProfileModel _ProfileModel { get; set; } = ProfileModel.ins;

        public Command FetchAvatarCM { get; set; }


        public ProfileViewModel()
        {

            DataProfile();


            //ProfileModel.Index = 1;
            ClickEdit();
            ClickLogout();
        }

        public void DataProfile()
        {
            _ProfileModel = ProfileModel.ins;
            NameProfile = _ProfileModel.NameProfile;
            StatusProfile = _ProfileModel.StatusProfile;
            UrlBackground = _ProfileModel.UrlBackground;
            UrlAvatar = _ProfileModel.Avatar;
            (string email, string pass) = SharedPreferenceService.ins.GetUserLogin();
            Email = email;
            NameFull = _ProfileModel.NameFull;
        }

        // click button edit
        public Command Edit_CM { get; set; }
        public void ClickEdit()
        {
            //EditProfileViewModel _dataEdit = new EditProfileViewModel();
            //NameProfile = _dataEdit.NameProfile;
            //StatusProfile = _dataEdit.StatusProfile;
            //UrlBackground = _dataEdit.UrlBackground;
            //UrlAvatar = _dataEdit.UrlAvatar;
            //Email = _dataEdit.Email;
            //NameFull = _dataEdit.NameFull;

            Edit_CM = new Command(async (p) =>
                       {
                           await Application.Current.MainPage.Navigation.PushAsync(new EditProfileScreen());
                           Image img = p as Image;
                           img.Source = _ProfileModel.Avatar;

                       });

            //this.Edit_CM = new Command(async () => {
            //     await Application.Current.MainPage.Navigation.PushAsync(new EditProfileScreen());
            // });
        }
        //logout button
        public Command Logout_CM { get; set; }
        public void ClickLogout()
        {
            // LogoutCM = new Command(() =>
            //            {
            //                SharedPreferenceService.ins.ClearUserLogin();
            //                SharedPreferenceService.ins.ClearUserToken();
            //                Application.Current.MainPage = new NavigationPage(new LoginScreen());
            //                App.Current.MainPage.Navigation.PopToRootAsync();
            //            });
            this.Logout_CM = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LoginScreen());
            });
        }

    }
}