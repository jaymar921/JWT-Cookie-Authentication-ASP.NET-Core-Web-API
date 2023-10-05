using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set;} = string.Empty;
        public string FavoriteColor { get; set; } = string.Empty;

        public AuthResponse(User user) 
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            FavoriteColor = user.FavoriteColor;
        }
    }
}
