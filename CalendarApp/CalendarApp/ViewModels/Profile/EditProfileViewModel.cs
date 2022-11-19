using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CalendarApp.Models.Profile;
using System.Windows.Input;

namespace CalendarApp.ViewModels.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {
        public ProfileModel ProfileModel { get; set; } = new ProfileModel();
        
        public ICommand MyEditProfile { get; set; }
        public EditProfileViewModel()
        {
            MyEditProfile = new Command(SaveProfile);
            
        }
        
        public void SaveProfile()
        {
            string _NameProfile = "test thu";
            string _StatusProfile = "ss";
            string _UrlBackground = "";
            string _UrlAvatar = "";
            if(MyEditProfile!=null)
            {
                 ProfileModel.NameProfile= _NameProfile;
                  ProfileModel.StatusProfile= _StatusProfile;
                 ProfileModel.UrlBackground= _UrlBackground;
                 ProfileModel.UrlAvatar= _UrlAvatar;
            }

            
            


            //    //submit new data
          // ProfileModel.ClearFields();
        }
        

    }
}
