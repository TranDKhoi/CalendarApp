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
    public partial class CustomReminderPopup : Popup
    {
        public CustomReminderPopup()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(null);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Hướng dẫn", "Ô đầu tiên hiển thị giá trị lặp và ô thứ 2 hiển thị đơn vị lặp. Ví dụ: 25 phút", "Đóng");
        }
    }
}