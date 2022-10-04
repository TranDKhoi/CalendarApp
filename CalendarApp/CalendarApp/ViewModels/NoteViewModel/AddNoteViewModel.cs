using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.NoteViewModel
{
    public class AddNoteViewModel : BaseViewModel
    {

        private ObservableCollection<Todo> todoList;
        public ObservableCollection<Todo> TodoList
        {
            get { return todoList; }
            set { todoList = value; OnPropertyChanged(); }
        }


        public Command AddNewNoteCM { get; set; }
        public Command AddMoreTodoCM { get; set; }

        public AddNoteViewModel()
        {
            TodoList = new ObservableCollection<Todo>();
            AddNewNoteCM = new Command(() =>
            {
                foreach (var item in TodoList)
                {
                    if (string.IsNullOrEmpty(item.description))
                        TodoList.Remove(item);
                }
                App.Current.MainPage.Navigation.PopAsync();
            });
            AddMoreTodoCM = new Command(() =>
            {
                TodoList.Add(new Todo());
            });
        }
    }
}
