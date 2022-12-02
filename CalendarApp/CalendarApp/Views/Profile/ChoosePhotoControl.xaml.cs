using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosePhotoControl : ContentView
    {
        public ChoosePhotoControl()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }
        public string Text
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }

        public string FontAwesomeText
        {
            get
            {
                return lblFontawesomeText.Text;
            }
            set
            {
                lblFontawesomeText.Text = value;
            }
        }
    }

}