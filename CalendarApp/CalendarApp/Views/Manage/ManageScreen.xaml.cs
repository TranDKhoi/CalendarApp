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

        }


    }
}