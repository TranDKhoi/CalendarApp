using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Manage
{
    public class CustomSaveAsDialogViewModel : BaseViewModel
    {
        private String selectedSaveAs;
        public String SelectedSaveAs
        {
            get { return selectedSaveAs; }
            set { selectedSaveAs = value; OnPropertyChanged(); }
        }

        public Command DoneCM { get; set; }

        public CustomSaveAsDialogViewModel()
        {
            SelectedSaveAs = "Chỉ sự kiện này";
            DoneCM = new Command((p) =>
            {
                Xamarin.CommunityToolkit.UI.Views.Popup popup = p as Popup;

                if (SelectedSaveAs == "Chỉ sự kiện này")
                {
                    SelectedSaveAs = "THIS";
                }
                else if (SelectedSaveAs == "Sự kiện này và theo sau")
                {
                    SelectedSaveAs = "THIS_AND_FOLLOWING";
                }
                else
                {
                    SelectedSaveAs = "ALL";
                }
                popup.Dismiss(SelectedSaveAs);

            });
        }
    }
}
