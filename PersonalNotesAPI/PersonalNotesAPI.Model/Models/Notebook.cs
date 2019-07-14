using PersonalNotesAPI.Model.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalNotesAPI.Model.Models
{
    public class Notebook : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual IEnumerable<Note> Notes { get; set; }
    }
}
