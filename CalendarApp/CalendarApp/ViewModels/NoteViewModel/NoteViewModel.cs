using Acr.UserDialogs;
using CalendarApp.Models;
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

        public NoteViewModel()
        {
            NoteList = new ObservableCollection<Note>();
            for (int i = 0; i < 5; i++)
            {
                List<NoteTodo> td = new List<NoteTodo>();
                td.Add(new NoteTodo { isDone = true, description = "Ăn cơm" });
                td.Add(new NoteTodo { isDone = false, description = "Ngủ trưa" });
                td.Add(new NoteTodo { isDone = true, description = "Học bài" });

                NoteList.Add(new Note
                {
                    title = "Một ngày bận rộn",
                    createdAt = DateTime.Now,
                    content = "Dọn dẹp nhà cửa đồ hết rồi mới được ngủ nghe chưa",
                    noteTodo = td,
                }); ;
            }

            SearchNoteCM = new Command((p) =>
            {
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
            ToAddNoteScreenCM = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new AddNoteScreen());

                //reload note listview
            });
            ClickNoteCM = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoteDetails(this));
                //reload note listview

            });
            SaveNoteCM = new Command(() =>
            {
                App.Current.MainPage.Navigation.PopAsync();
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
                            App.Current.MainPage.Navigation.PopAsync();
                    },
                });
            });
        }
    }
}
