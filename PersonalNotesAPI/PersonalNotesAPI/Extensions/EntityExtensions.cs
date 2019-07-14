using PersonalNotesAPI.Model.Models;
using PersonalNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalNotesAPI.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateNotebook(this Notebook notebook, NotebookVM notebookVM)
        {
            notebook.Name = notebookVM.Name;
            notebook.ParentId = notebookVM.ParentId;
            notebook.Timestamp = notebookVM.Timestamp;
        }
        public static void UpdateNote(this Note note, NoteVM noteVM)
        {
            note.Title = noteVM.Title;
            note.Description = noteVM.Description;
            note.Finished = noteVM.Finished;
            note.Timestamp = noteVM.Timestamp;
            note.NotebookId = noteVM.NotebookId;
        }
    }
}
