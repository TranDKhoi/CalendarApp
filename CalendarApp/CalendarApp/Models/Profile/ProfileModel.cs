using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xam.TabView;
using Xam.TabView.Control;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
namespace CalendarApp.Models.Profile
{
    public class ProfileModel : BaseViewModel
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
        public ProfileModel() 
        {
            NameProfile = "Phap";
            StatusProfile = "Mai mai ben nhau";
            UrlBackground = "avatar.jpg";
            UrlAvatar = "anh.jpg";
            Email = "ngocphap5@gmail.com";
            NameFull = "Huỳnh Ngọc Pháp";
        }
        public ProfileModel(string _nameProfile, string _statusProfile, string _urlBackground, string _urlAvatar , string _email , string _nameFull) 
        {
            nameProfile = _nameProfile;
            statusProfile = _statusProfile;
            urlBackground = _urlBackground;
            urlAvatar = _urlAvatar;
            email = _email;
            nameFull = _nameFull;
        }



    }
}
