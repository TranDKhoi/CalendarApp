using CalendarApp.Models.Profile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace CalendarApp.Services
{
    public class SharedPreferenceService
    {
        private static SharedPreferenceService _ins;
        public static SharedPreferenceService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new SharedPreferenceService();
                return _ins;
            }
            set { _ins = value; }
        }


        public void SetUserData(ProfileModel user)
        {
            Preferences.Set("UserData", JsonConvert.SerializeObject(user));
        }






        public void SetUserToken(string token)
        {
            Preferences.Set("user_token", token);
        }

        public string GetUserToken()
        {
            return Preferences.Get("user_token", null);
        }

        public void ClearUserToken()
        {
            Preferences.Remove("user_token");
        }

        public void SetUserLogin(string email, string password)
        {
            Preferences.Set("user_email", email);
            Preferences.Set("user_password", password);
        }

        public (string, string) GetUserLogin()
        {
            return (Preferences.Get("user_email", null), Preferences.Get("user_password", null));
        }

        public void ClearUserLogin()
        {
            Preferences.Remove("user_email");
            Preferences.Remove("user_password");
        }
    }
}
