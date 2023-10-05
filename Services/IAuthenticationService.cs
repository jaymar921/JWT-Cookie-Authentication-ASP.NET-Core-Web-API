using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Models;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services
{
    public interface IAuthenticationService
    {
        AuthResponse? Authenticate(AuthRequest requestModel);
    }
}
