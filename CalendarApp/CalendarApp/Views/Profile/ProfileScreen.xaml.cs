using CalendarApp.Models.Profile;
using CalendarApp.ViewModels.Manage;
using CalendarApp.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileScreen : ContentPage
    {
        public ProfileScreen()
        {

            InitializeComponent();
            // this.BindingContext = BindingContext;
        }
        int cout = 0;
        protected override void OnAppearing()
        {
            try
            {
                var vm = (ProfileViewModel)this.BindingContext;
                vm.DataProfile();
                avtImg.Source = ImageSource.FromStream(() => ProfileModel.ins.UrlAvatar);
            }
            catch (Exception)
            {
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //avtImg.Source = ProfileModel.ins.Avatar;
            //bgImg.Source = ProfileModel.ins.UrlBackground;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                double _scale = 0;
                _scale = gridImageOption.Scale;
                await gridImageOption.ScaleTo(0, 500);
                gridImageOption.IsVisible = true;
                await gridImageOption.ScaleTo(_scale, 300);
                cout = 1;
            }
            catch (Exception)
            {
            }
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                double _scale = 0;
                _scale = gridImageOption.Scale;
                await gridImageOption.ScaleTo(0, 500);
                gridImageOption.IsVisible = true;
                await gridImageOption.ScaleTo(_scale, 300);
                cout = 2;
            }
            catch (Exception)
            {
            }
            
        }
        private async void ctrlCamera_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    if (cout == 1)
                    {
                        avtImg.Source = ImageSource.FromStream(() => stream);
                        ProfileModel.ins.UrlAvatar = stream;
                    }
                    else
                    {
                        bgImg.Source = ImageSource.FromStream(() => stream);
                        ProfileModel.ins.UrlAvatar = stream;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }
        private async void ctrlGallery_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    if (cout == 1)
                    {
                        avtImg.Source = ImageSource.FromStream(() => stream);
                    }
                    else
                    {
                        bgImg.Source = ImageSource.FromStream(() => stream);
                    }
                    CloseDialog();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CloseDialog();
        }
        private void CloseDialog()
        {
            gridImageOption.IsVisible = false;
        }
    }


}