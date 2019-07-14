using System.ComponentModel.DataAnnotations;

namespace PersonalNotesAPI.Models
{
    public class RegisterVM
    {
        [MaxLength(256, ErrorMessage = "Fullname length can't be more than 256.")]
        [Required(ErrorMessage = "Fullname is required field")]
        public string FullName { get; set; }
        //[RegularExpression("^[a - z][a - z0 - 9_\\.]{5, 32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$/gm",ErrorMessage ="Incorrect Email")]
        [MaxLength(256, ErrorMessage = "Email length can't be more than 256.")]
        [Required(ErrorMessage = "Email is required field")]
        public string Email { get; set; }
        [MaxLength(256, ErrorMessage = "Password length can't be more than 256.")]
        [Required(ErrorMessage = "Password is required field")]
        public string Password { get; set; }
    }
}
