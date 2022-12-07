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
    public partial class SelectDayOfWeekPopup : Popup
    {
        public SelectDayOfWeekPopup()
        {
            InitializeComponent();
        }
        private void ListDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ListDay.SelectedItem == null)
                {
                    return;
                }
                var viewModel = (SelectDayOfWeekViewModel)this.BindingContext;
                viewModel.SelectDayCM.Execute(ListDay.SelectedItem);
                ListDay.SelectedItem = null;
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