using System;
using System.ComponentModel.DataAnnotations;
using PersonalNotesAPI.Model.Abstracts;

namespace PersonalNotesAPI.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        [Required]
        [MaxLength(50)]
        public string CreatedBy { get ; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        [MaxLength(50)]
        public string UpdatedBy { get ; set; }
        public DateTime? UpdatedOn { get ; set; }
        public bool Deleted { get; set; }
    }
}
