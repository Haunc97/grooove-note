using System;

namespace PersonalNotesAPI.Models
{
    public class TokenVM
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserEmail { get; set; }
    }
}
