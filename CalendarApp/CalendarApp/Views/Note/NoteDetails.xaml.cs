using CalendarApp.ViewModels.NoteViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarApp.Views.Note
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetails : ContentPage
    {
        public NoteDetails(NoteViewModel context)
        {
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}