using PersonalNotesAPI.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalNotesAPI.Model.Models
{
    public class Note : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Finished { get; set; }

        public int NotebookId { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [ForeignKey("NotebookId")]
        public virtual Notebook Notebook { get; set; }
    }
}
