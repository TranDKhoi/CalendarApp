using CalendarApp.Models.Profile;
using CalendarApp.Services;
using CalendarApp.ViewModels.Profile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileScreen : ContentPage
    {
        public EditProfileScreen()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
            }
        }

        int cout = 0;
        private async void btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                double _scale = 0;
                _scale = gridImageOption.Scale;
                sl.IsEnabled = false;
                await gridImageOption.ScaleTo(0, 500);
                gridImageOption.IsVisible = true;
                await gridImageOption.ScaleTo(_scale, 300);
                cout = 1;
            }
            catch (Exception)
            {
            }
        }
        private async void btn_Clicked1(object sender, EventArgs e)
        {
            try
            {
                double _scale = 0;
                _scale = gridImageOption.Scale;
                sl.IsEnabled = false;
                await gridImageOption.ScaleTo(0, 500);
                gridImageOption.IsVisible = true;
                await gridImageOption.ScaleTo(_scale, 300);
                cout = 2;
            }
            catch (Exception)
            {
            }
        }


        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            sl.IsEnabled = true;
            gridImageOption.IsVisible = false;

        }

        private async void ctrlCamera_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    // img.Source = ImageSource.FromStream(() => stream);

                    if (cout == 1)

                    {
                        img.Source = ImageSource.FromStream(() => stream);
                        ProfileModel.ins.UrlAvatar = stream;
                    }
                    else

                    {
                        img_1.Source = ImageSource.FromStream(() => stream);
                        ProfileModel.ins.UrlAvatar = stream;
                    }

                }
                CloseDialog();
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
                    //img.Source = ImageSource.FromStream(() => stream);

                    if (cout == 1)
                    {
                        ProfileModel.ins.Avatar = ImageSource.FromStream(() => stream);
                        img.Source = ProfileModel.ins.Avatar;
                    }


                    else

                    {
                        ProfileModel.ins.UrlBackground = ImageSource.FromStream(() => stream);
                        img_1.Source = ProfileModel.ins.UrlBackground;
                    }
                    CloseDialog();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }


    }
}