using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void SetProperty(string prop, string val)
        {

        }
        public void ClearFields()
        {
            NameProfile = string.Empty;
            StatusProfile = string.Empty;
            UrlBackground = string.Empty;
            UrlAvatar = string.Empty;
        }
        
        

    }
}
