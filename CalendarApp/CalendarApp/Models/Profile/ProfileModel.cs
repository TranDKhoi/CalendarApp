using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Models.Profile
{
    public class ProfileModel : BaseViewModel
    {
        private static ProfileModel _ins;

        public static ProfileModel ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ProfileModel();
                return _ins;
            }
            set { _ins = value; }
        }


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
        Stream urlAvatar;
        public Stream UrlAvatar
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
            NameProfile = "Name User";
            StatusProfile = "Cau chuyen tam dac";
            UrlBackground = "AvatarDemo.jpg";
            //UrlAvatar = "anh.jpg";
            Email = "ngocphap5@gmail.com";
            NameFull = "Full name";
        }


    }
}
