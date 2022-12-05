using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views.Note;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.NoteViewModel
{
    public class NoteViewModel : BaseViewModel
    {

        private ObservableCollection<Note> noteList;
        public ObservableCollection<Note> NoteList
        {
            get { return noteList; }
            set { noteList = value; OnPropertyChanged(); }
        }

        private string searchNoteText;
        public string SearchNoteText
        {
            get { return searchNoteText; }
            set { searchNoteText = value; OnPropertyChanged(); }
        }

        private Note selectedNote;
        public Note SelectedNote
        {
            get { return selectedNote; }
            set { selectedNote = value; OnPropertyChanged(); }
        }

        public Command ClickNoteCM { get; set; }
        public Command SearchNoteCM { get; set; }
        public Command ToAddNoteScreenCM { get; set; }
        public Command SaveNoteCM { get; set; }
        public Command DeleteNoteCM { get; set; }
        public Command ReFetchNoteCM { get; set; }

        public NoteViewModel()
        {
            ReFetchNoteCM = new Command(() =>
            {
                var res = NoteService.ins.GetAllNote();
                if (res != null)
                {
                    NoteList = new ObservableCollection<Note>(res);
                }
            });
            SearchNoteCM = new Command((p) =>
            {
                if (NoteList == null || NoteList.Count == 0) return;

                CollectionView cl = p as CollectionView;

                ObservableCollection<Note> searchNote = new ObservableCollection<Note>();

                if (!string.IsNullOrEmpty(SearchNoteText))
                {

                    for (int i = 0; i < NoteList.Count; i++)
                    {
                        if (NoteList[i].title.ToLower().Contains(SearchNoteText.ToLower()))
                        {
                            searchNote.Add(NoteList[i]);
                        }
                    }
                    cl.ItemsSource = searchNote;

                }
                else
                {
                    cl.ItemsSource = NoteList;
                }

            });
            ToAddNoteScreenCM = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new AddNoteScreen());
            });
            ClickNoteCM = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new NoteDetails(this));
            });
            SaveNoteCM = new Command(() =>
            {
                var res = NoteService.ins.UpdateNote(SelectedNote);
                if (res)
                {
                    App.Current.MainPage.Navigation.PopAsync();
                    UserDialogs.Instance.Toast("Cập nhật thành công");
                }
                else
                {
                    UserDialogs.Instance.Toast("Lỗi hệ thống");
                }
            });
            DeleteNoteCM = new Command(() =>
            {
                UserDialogs.Instance.Confirm(new ConfirmConfig
                {
                    Message = "Bạn có chắc muốn xoá ghi chú này không?",
                    OkText = "Có",
                    CancelText = "Không",
                    Title = "Xoá ghi chú",
                    OnConfirm = (isYes) =>
                    {
                        //Delete here
                        if (isYes)
                        {
                            NoteService.ins.DeleteNote(SelectedNote);
                            App.Current.MainPage.Navigation.PopAsync();
                            UserDialogs.Instance.Toast("Xoá thành công");
                        }
                    },
                });
            });
        }
    }
}
