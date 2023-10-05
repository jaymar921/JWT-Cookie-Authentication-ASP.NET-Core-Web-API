using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;
using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Helpers;
using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Models;
using Microsoft.Extensions.Options;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthResponse? Authenticate(AuthRequest requestModel)
        {
            // In production, this will be retrieving user data in a database
            User? user = _userRepository.GetAllUsers().SingleOrDefault(u => u.Username == requestModel.Username && u.Password == requestModel.Password);

            // return null if user is not found
            if (user == null) return null;

            string token = JWTHelper.GenerateJsonWebToken(user, _appSettings);

            return new AuthResponse(user, token);
        }
    }
}
