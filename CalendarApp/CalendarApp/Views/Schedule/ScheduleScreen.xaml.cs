using CalendarApp.Models;
using CalendarApp.ViewModels.Converter;
using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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
            try
            {
                if (ListDay.SelectedItem == null)
                {
                    return;
                }
                var viewModel = (ScheduleViewModel)this.BindingContext;
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

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CollectionViewTask.SelectedItem == null)
                {
                    return;
                }
                var viewModel = (ScheduleViewModel)this.BindingContext;
                viewModel.SelectTaskCM.Execute(CollectionViewTask.SelectedItem);
                CollectionViewTask.SelectedItem = null;
            }
            catch (Exception)
            {
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var vm = (ScheduleViewModel)this.BindingContext;
                vm.GetAllTaskCM.Execute(null);
            }
            catch (Exception)
            {
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                if (((Label)sender).BindingContext is Event selectedItem)
                {
                    var viewModel = (ScheduleViewModel)this.BindingContext;
                    viewModel.SelectRestDayCM.Execute(selectedItem);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}