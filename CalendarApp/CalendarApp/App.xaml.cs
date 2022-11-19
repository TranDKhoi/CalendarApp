﻿using CalendarApp.Views.Authen;
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
            MainPage = new NavigationPage(new LoginScreen());
            //MainPage = new NavigationPage(new BottomBarCustom());
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
