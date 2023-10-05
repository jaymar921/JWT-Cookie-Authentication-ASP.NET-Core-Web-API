using System.Text.Json.Serialization;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FavoriteColor { get; set; } = string.Empty;
        public string Username { get; set;} = string.Empty;

        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
    }
}
