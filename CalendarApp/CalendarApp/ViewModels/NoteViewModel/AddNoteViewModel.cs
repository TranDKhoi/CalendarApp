using Acr.UserDialogs;
using CalendarApp.Models;
using CalendarApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace CalendarApp.ViewModels.NoteViewModel
{
    public class AddNoteViewModel : BaseViewModel
    {

        private ObservableCollection<NoteTodo> todoList;
        public ObservableCollection<NoteTodo> TodoList
        {
            get { return todoList; }
            set { todoList = value; OnPropertyChanged(); }
        }


        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }




        public Command AddNewNoteCM { get; set; }
        public Command AddMoreTodoCM { get; set; }

        public AddNoteViewModel()
        {
            TodoList = new ObservableCollection<NoteTodo>();
            AddNewNoteCM = new Command(() =>
            {
                Note newNote = new Note
                {
                    title = Title,
                    content = Content,
                    noteTodo = new List<NoteTodo>(TodoList.Where(i => !string.IsNullOrEmpty(i.description)).ToList()),
                    createdAt = DateTime.Now,
                };

                var res = NoteService.ins.CreateNewNote(newNote);
                if (res)
                {
                    App.Current.MainPage.Navigation.PopAsync();
                    UserDialogs.Instance.Toast("Thêm thành công");
                }
                else
                {
                    UserDialogs.Instance.Toast("Lỗi hệ thống");
                }

            });
            AddMoreTodoCM = new Command(() =>
            {
                TodoList.Add(new NoteTodo());
            });
        }
    }
}
