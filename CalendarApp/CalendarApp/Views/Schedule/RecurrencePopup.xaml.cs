using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecurrencePopup : Popup
    {
        public RecurrencePopup(DateTime today)
        {
            InitializeComponent();
            var viewModel = (RecurrenceViewModel)this.BindingContext;
            viewModel.Today = today;
        }

        private void ListDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ListDay.SelectedItem == null)
                {
                    return;
                }
                var viewModel = (RecurrenceViewModel)this.BindingContext;
                viewModel.SelectDayCM.Execute(ListDay.SelectedItem);
                ListDay.SelectedItem = null;
            }
            catch (Exception)
            {
            }
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                datePicker.Focus();
            }
            catch (Exception)
            {
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.Dismiss(null);
            }
            catch (Exception)
            {
            }
        }
    }
}