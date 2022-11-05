using CalendarApp.ViewModels.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Authen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassScreen : ContentPage
    {
        public ResetPassScreen(ForgotPassViewModel context)
        {
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}