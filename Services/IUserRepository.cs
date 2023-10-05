using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services
{
    public interface IUserRepository
    {
        /*
         * For simplicity purpose, lets just hard code the data
         * and just retrieve a list of users in a hard coded database
         */
        public IEnumerable<User> GetAllUsers();
    }
}
