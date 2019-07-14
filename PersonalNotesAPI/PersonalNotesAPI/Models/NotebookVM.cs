using PersonalNotesAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalNotesAPI.Models
{
    public class NotebookVM
    {        
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [Required(ErrorMessage = "The 'Name' field is required")]
        public string Name { get; set; }

        [ParentId]
        public int? ParentId { get; set; }

        public bool Deleted { get; set; }  
        
        public byte[] Timestamp { get; set; }

        // Auditable
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual IEnumerable<NoteVM> Notes { get; set; }
    }
}
