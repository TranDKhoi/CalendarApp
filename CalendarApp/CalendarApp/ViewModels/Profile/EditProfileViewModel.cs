using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CalendarApp.Models.Profile;
using System.Windows.Input;
using Xamarin.Essentials;
using CalendarApp.Views.Profile;

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
        public ProfileViewModel _ProfileViewModel { get; set; } = new ProfileViewModel();
        
        public EditProfileViewModel()
        {
            

            NameProfile = _ProfileViewModel.NameProfile;
            StatusProfile = _ProfileViewModel.StatusProfile;
            UrlBackground = _ProfileViewModel.UrlBackground;
            UrlAvatar = _ProfileViewModel.UrlAvatar;
            Email = _ProfileViewModel.Email;
            NameFull = _ProfileViewModel.NameFull;

           // CM_MyEditProfile = new Command(SaveProfile);
          
            SaveProfile();

        }

        public void ClearFields()
        {
            _ProfileViewModel.NameProfile = string.Empty;
            _ProfileViewModel.StatusProfile = string.Empty;
            _ProfileViewModel.UrlBackground = string.Empty;
            _ProfileViewModel.UrlAvatar = string.Empty;
            _ProfileViewModel.Email = string.Empty;
        }
       
        public void SaveProfile()
        {


            //string _nameProfile = "ngocphap1";
            //string _statusProfile = "yeunhaunhieu";
            //string _urlBackground = "";

            //string _urlAvatar = "";
            //string _email = "11111111@gmail.com";
            //string _nameFull = "micosss";
            //if (CM_MyEditProfile != null)
            //{
            //    _ProfileViewModel.NameProfile = _nameProfile;
            //    _ProfileViewModel.StatusProfile = _statusProfile;
            //    _ProfileViewModel.UrlBackground = _urlBackground;
            //    _ProfileViewModel.UrlAvatar = _urlAvatar;
            //    _ProfileViewModel.Email = _email;
            //    _ProfileViewModel.NameFull = _nameFull;

            //}

            this.CM_MyEditProfile = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ProfileScreen());
            });


            //    //submit new data
            //ProfileViewModel.ClearFields();
        }


    }
}
