using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPopup : Popup
    {
        public AddTaskPopup()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            endDatePicker.Focus();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Hướng dẫn", "1 tiết học kéo dài 45p", "Đóng");
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Hướng dẫn", "Nếu không có ngày kết thúc thì sử dụng tổng số tiết và ngược lại", "Đóng");
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Hướng dẫn", "Chọn màu thẻ hiển thị trên lịch", "Đóng");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(null);
        }
    }
}