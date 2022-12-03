using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CalendarApp.Models.Profile;
using System.Windows.Input;
using Xamarin.Essentials;
using CalendarApp.Views.Profile;
using CalendarApp.Services;

namespace CalendarApp.ViewModels.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {


        public ICommand CM_MyEditProfile { get; set; }
        public ICommand EditAvatar_CM { get; set; }
        public ICommand EditBackground_CM { get; set; }

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
        ImageSource urlAvatar;
        public ImageSource UrlAvatar
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
        public ProfileModel profileModel { get; set; } = ProfileModel.ins;

        public EditProfileViewModel()
        {


            NameProfile = profileModel.NameProfile;
            StatusProfile = profileModel.StatusProfile;
            UrlBackground = profileModel.UrlBackground;
            UrlAvatar = ImageSource.FromStream(() => profileModel.UrlAvatar);
            (string email, string pass) = SharedPreferenceService.ins.GetUserLogin();
            Email = email;
            NameFull = profileModel.NameFull;

            CM_MyEditProfile = new Command(() =>
            {


                profileModel.NameProfile = NameProfile;
                profileModel.StatusProfile = StatusProfile;
                profileModel.UrlBackground = UrlBackground;
                profileModel.Email = Email;
                profileModel.NameFull = NameFull;

                Application.Current.MainPage.Navigation.PopAsync();

            });

            // SaveProfile();

        }

        public void ClearFields()
        {
            //profileModel.NameProfile = string.Empty;
            //profileModel.StatusProfile = string.Empty;
            //profileModel.UrlBackground = string.Empty;
            //profileModel.UrlAvatar = string.Empty;
            //profileModel.Email = string.Empty;
        }


    }
}
