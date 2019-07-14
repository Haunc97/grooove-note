using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalNotesAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        [Required]
        public string FullName { get; set; }
    }
}
