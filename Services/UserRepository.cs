using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services
{
    public class UserRepository : IUserRepository
    {
        /*
         * For simplicity sake, we will just hard code the entries
         * but in real life scenario, the data will be in the database
         */
        private List<User> users;

        public UserRepository() 
        {
            users = new List<User>();
            // load the data 
            InitializeEnties();
        }

        private void InitializeEnties()
        {
            /*
             * Hard coded entries that will be used for database lookup,
             * note that it is a best practice to hash the password and don't expose the
             * plain text password
             */
            users.Add(new User { Id = 1, Name = "Jayharron Mar Abejar", Email = "jay@email.com", FavoriteColor = "Blue", Username = "jay123", Password = "jay456"});
            users.Add(new User { Id = 2, Name = "Pia Abellana", Email = "pia@email.com", FavoriteColor = "Red", Username = "pia123", Password = "pia456" });
            users.Add(new User { Id = 3, Name = "Rey Vincent De los Reyes", Email = "rey@email.com", FavoriteColor = "Green", Username = "rey123", Password = "rey456" });
            users.Add(new User { Id = 4, Name = "James Dylan Caramonte", Email = "james@email.com", FavoriteColor = "Red", Username = "james123", Password = "james456" });
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }
    }
}
