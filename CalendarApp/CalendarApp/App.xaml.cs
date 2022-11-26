using CalendarApp.Services;
using CalendarApp.Views.Authen;
using CalendarApp.Views.BottomBarCustom;
using System.Globalization;
using Xamarin.Forms;

namespace CalendarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            (string email, string password) = SharedPreferenceService.ins.GetUserLogin();
            if (email != null && password != null)
                MainPage = new NavigationPage(new BottomBarCustom());
            else
                MainPage = new NavigationPage(new LoginScreen());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
