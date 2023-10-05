using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(userRepository.GetAllUsers());
        }
    }
}
