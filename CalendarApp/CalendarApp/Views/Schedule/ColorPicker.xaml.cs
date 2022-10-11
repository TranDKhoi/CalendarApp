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
    public partial class ColorPicker : Popup
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(null);
        }
    }
}