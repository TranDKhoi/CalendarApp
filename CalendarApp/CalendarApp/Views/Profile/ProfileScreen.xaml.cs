﻿using CalendarApp.Models.Profile;
using CalendarApp.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileScreen : ContentPage
    {
        public ProfileScreen()
        {

            InitializeComponent();
            // this.BindingContext = BindingContext;
        }
    }
       
    
}