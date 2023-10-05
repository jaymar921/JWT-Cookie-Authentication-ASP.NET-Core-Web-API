using System.ComponentModel.DataAnnotations;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Models
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
