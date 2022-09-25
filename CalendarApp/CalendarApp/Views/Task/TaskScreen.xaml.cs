using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Task
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskScreen : ContentPage
    {
        public TaskScreen()
        {
            InitializeComponent();
        }
    }
}