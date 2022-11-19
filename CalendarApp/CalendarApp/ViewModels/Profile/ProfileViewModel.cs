using CalendarApp.Models.Profile;
using CalendarApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace CalendarApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileModel ProfileModel { get;  } = new ProfileModel();
        public Command EditProfileScreenNavCommand { get; set; }
        public ProfileViewModel()
        {

            ProfileModel.NameProfile = "Phap";
            ProfileModel.StatusProfile = "Mai mai en nhau";
            ProfileModel.UrlBackground = "avatar.jpg";
            ProfileModel.UrlAvatar = "anh.jpg";
            this.EditProfileScreenNavCommand = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new EditProfileScreen());
            });
           
        }

        
    }
}