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
    public partial class NoteScreen : ContentPage
    {
        public NoteScreen()
        {
            InitializeComponent();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listNote.SelectedItem == null) return;
                var viewModel = (NoteViewModel)this.BindingContext;
                viewModel.SelectedNote = (Models.Note)listNote.SelectedItem;
                viewModel.ClickNoteCM.Execute(listNote.SelectedItem);
                listNote.SelectedItem = null;
            }
            catch (Exception)
            {
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (NoteViewModel)this.BindingContext;
            viewModel.ReFetchNoteCM.Execute(null);
        }
    }
}