using CalendarApp.Models.Profile;
using CalendarApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xam.TabView;
using Xam.TabView.Control;
using CalendarApp.Views.Authen;
using Xamarin.Essentials;
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
        string urlBackground;
        public string UrlBackground
        {
            get { return urlBackground; }
            set { urlBackground = value; OnPropertyChanged(nameof(UrlBackground)); }
        }
        string urlAvatar;
        public string UrlAvatar
        {
            get { return urlAvatar; }
            set { urlAvatar = value; OnPropertyChanged(nameof(UrlAvatar)); }
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
            set { nameFull = value; OnPropertyChanged(nameof(nameFull)); }
        }


        public ProfileModel _ProfileModel { get; set; } = new ProfileModel() { };
        

        public ProfileViewModel()
        {

            DataProfile();


            //ProfileModel.Index = 1;
            ClickEdit();
            ClickLogout();


        }

       public void DataProfile()
        {
            NameProfile = _ProfileModel.NameProfile;
            StatusProfile = _ProfileModel.StatusProfile;
            UrlBackground = _ProfileModel.UrlBackground;
            UrlAvatar = _ProfileModel.UrlAvatar;
            Email = _ProfileModel.Email;
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
           


            this.Edit_CM = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new EditProfileScreen());
            });
        }
        //logout button
        public Command Logout_CM { get; set; }
        public void ClickLogout()
        {
            this.Logout_CM = new Command( () => {
                SharedPreferenceService.ins.ClearUserToken();
                SharedPreferenceService.ins.ClearUserLogin();
                Application.Current.MainPage = new NavigationPage(new LoginScreen());
            });
        }

    }
}