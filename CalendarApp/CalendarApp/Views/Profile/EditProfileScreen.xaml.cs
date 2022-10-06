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

            InitializeComponent();
           

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var pickImage = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle="Pick an Image"
            }) ;
            if(pickImage !=null)
            {
                var stream = await pickImage.OpenReadAsync();
                imgFile.Source = ImageSource.FromStream(() => stream);
                fileName.Text = pickImage.FileName;
            }
        }
    }
}