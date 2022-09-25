using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleScreen : ContentPage
    {
        public ScheduleScreen()
        {
            InitializeComponent();
        }

        private void listDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListDay.SelectedItem == null)
            {
                return;
            }
            var viewModel = (ScheduleViewModel)this.BindingContext;
            viewModel.SelectDayCM.Execute(ListDay.SelectedItem);
            ListDay.SelectedItem = null;
        }
    }
}