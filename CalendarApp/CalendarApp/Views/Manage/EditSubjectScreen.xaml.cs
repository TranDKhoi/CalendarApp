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
    public partial class EditSubjectScreen : ContentPage
    {
        public EditSubjectScreen(ManageViewModel context)
        {
            this.BindingContext = context;
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
    }
}