using CalendarApp.ViewModels.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Manage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSaveAsDialog : Popup
    {
        public CustomSaveAsDialog()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(null);
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            var vm = (CustomSaveAsDialogViewModel)this.BindingContext;
            vm.SelectedSaveAs = rd.Content.ToString();
        }
    }
}