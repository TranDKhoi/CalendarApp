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
    public partial class VerifyForgot : ContentPage
    {
        public VerifyForgot(ForgotPassViewModel BindingContext)
        {
            this.BindingContext = BindingContext;
            InitializeComponent();
        }
    }
}