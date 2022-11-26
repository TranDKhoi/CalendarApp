using CalendarApp.Models.Profile;
using CalendarApp.ViewModels.Profile;
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
        //public ProfileViewModel ProfileViewModel = new ProfileViewModel();
        //public ProfileModel ProfileModel = new ProfileModel();


        public EditProfileScreen( )
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
            double _scale = 0;
            _scale = gridImageOption.Scale;
            sl.IsEnabled = false;
            await gridImageOption.ScaleTo(0, 500);
            gridImageOption.IsVisible = true;
            await gridImageOption.ScaleTo(_scale, 300);
            cout = 1;
            
        }
        private async void btn_Clicked1(object sender, EventArgs e)
        {
            double _scale = 0;
            _scale = gridImageOption.Scale;
            sl.IsEnabled = false;
            await gridImageOption.ScaleTo(0, 500);
            gridImageOption.IsVisible = true;
            await gridImageOption.ScaleTo(_scale, 300);
            cout = 2;

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

                    {  img.Source = ImageSource.FromStream(() => stream); }

                    else

                    {  img_1.Source = ImageSource.FromStream(() => stream); }



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

                    { img.Source = ImageSource.FromStream(() => stream); }

                    else

                    { img_1.Source = ImageSource.FromStream(() => stream); }



                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }

       
    }
}