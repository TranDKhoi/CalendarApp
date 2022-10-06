using System;
using System.Collections.Generic;
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
    }
}
