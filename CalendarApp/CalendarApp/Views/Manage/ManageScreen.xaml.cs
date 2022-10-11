using CalendarApp.ViewModels.Manage;
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
    public partial class ManageScreen : ContentPage
    {
        public ManageScreen()
        {
            InitializeComponent();
            var vm = (ManageViewModel)this.BindingContext;
            vm.FirstLoadSubjectCM.Execute(null);
        }

        private void TabView_SelectionChanged(object sender, Xamarin.CommunityToolkit.UI.Views.TabSelectionChangedEventArgs e)
        {
            Xamarin.CommunityToolkit.UI.Views.TabView tabView = (Xamarin.CommunityToolkit.UI.Views.TabView)sender;
            var vm = (ManageViewModel)this.BindingContext;

            if (tabView.SelectedIndex == 0)
            {
                vm.FirstLoadSubjectCM.Execute(null);
            }
            if (tabView.SelectedIndex == 1)
            {
                vm.FirstLoadTaskCM.Execute(null);
            }
        }

        private void listSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listSubject.SelectedItem == null) return;
            var vm = (ManageViewModel)this.BindingContext;
            vm.SelectedSubject = (Models.Subject)listSubject.SelectedItem;
            vm.ToEditSubjectCM.Execute(null);
            listSubject.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            var vm = (ManageViewModel)this.BindingContext;
            vm.FirstLoadSubjectCM.Execute(null);
            vm.FirstLoadTaskCM.Execute(null);
        }

        private void listTask_SelectionChaned2(object sender, SelectionChangedEventArgs e)
        {
            if (listTask.SelectedItem == null) return;
            var vm = (ManageViewModel)this.BindingContext;
            vm.SelectedTodo = (Models.Todo)listTask.SelectedItem;
            vm.ToEditTodoCM.Execute(null);
            listTask.SelectedItem = null;
        }
    }
}