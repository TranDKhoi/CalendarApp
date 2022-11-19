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
            DoneCM = new Command((p) =>
            {
                Xamarin.CommunityToolkit.UI.Views.Popup popup = p as Popup;
                if (!string.IsNullOrEmpty(SelectedSaveAs))
                {
                    popup.Dismiss(SelectedSaveAs);
                }
            });
        }
    }
}
