using CalendarApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Linq;

namespace CalendarApp.Services
{
    public class NoteService
    {
        static private NoteService _ins;
        static public NoteService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new NoteService();
                return _ins;
            }
            set { _ins = value; }
        }

        private List<Note> allNote { get; set; }


        NoteService()
        {
            allNote = new List<Note>();
        }

        public List<Note> GetAllNote()
        {
            List<Note> notes = new List<Note>();
            var res = Preferences.Get("note", null);
            if (res != null)
            {
                notes = new List<Note>(JsonConvert.DeserializeObject<List<Note>>(res));
                allNote = notes;
                return allNote;
            }
            return null;
        }

        public bool CreateNewNote(Note newNote)
        {
            try
            {
                allNote.Add(newNote);
                Preferences.Set("note", JsonConvert.SerializeObject(allNote));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteNote(Note delNote)
        {
            try
            {
                allNote.Remove(delNote);
                Preferences.Set("note", JsonConvert.SerializeObject(allNote));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateNote(Note upNote)
        {
            try
            {
                for (int i = 0; i < allNote.Count; i++)
                {
                    if (allNote[i].createdAt == upNote.createdAt)
                    {
                        allNote[i] = upNote;
                    }
                }
                Preferences.Set("note", JsonConvert.SerializeObject(allNote));
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
