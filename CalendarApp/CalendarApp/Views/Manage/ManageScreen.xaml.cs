using CalendarApp.Models;
using CalendarApp.ViewModels.Manage;
using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Manage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageScreen : TabbedPage
    {
        public ManageScreen()
        {
            InitializeComponent();
            var vm = (ManageViewModel)this.BindingContext;
            vm.FirstLoadSubjectCM.Execute(null);
        }

        private void listSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listSubject.SelectedItem == null) return;
                var vm = (ManageViewModel)this.BindingContext;
                vm.SelectedSubject = (Models.Subject)listSubject.SelectedItem;
                vm.ToEditSubjectCM.Execute(null);
                listSubject.SelectedItem = null;
            }
            catch (Exception)
            {
            }
            
        }

        protected override void OnAppearing()
        {
            try
            {
                var vm = (ManageViewModel)this.BindingContext;
                vm.FirstLoadSubjectCM.Execute(null);
            }
            catch (Exception)
            {
            }
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (((Label)sender).BindingContext is DayOffSubject selectedItem)
                {
                    var viewModel = (ManageViewModel)this.BindingContext;
                    viewModel.DeleteDayOffCM.Execute(selectedItem);
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void listDayOff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listDayOff.SelectedItem == null) return;
                listDayOff.SelectedItem = null;
            }
            catch (Exception)
            {
            }
            
        }
    }
}