using Microsoft.AspNetCore.Mvc;
using PersonalNotesAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalNotesAPI.Models
{
    public class NoteVM
    {      
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Title length can't be more than 50.")]
        [Required(ErrorMessage = "The title field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description field is required")]
        public string Description { get; set; }

        public bool Finished { get; set; }

        [NotebookForeign]
        public int NotebookId { get; set; }

        public bool Deleted { get; set; }

        public byte[] Timestamp { get; set; }

        // Auditable
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // public virtual NotebookVM Notebook { get; set; }
    }
}
