using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private Services.IAuthenticationService authenticationService;

        public AuthenticationController(Services.IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        // Login
        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            // Authenticate the user and get the response
            AuthResponse? response = authenticationService.Authenticate(authRequest);

            if(response == null)
            {
                return Unauthorized(new { Message = "Invalid Login Credentials" });
            }

            // create the userclaims
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Name),

                // specify custom claims
                new Claim("token", response.Token)
            };

            // creating the identity
            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            // creating the principal object with the identity
            var principal = new ClaimsPrincipal(identity);

            // settings for the authentication properties
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            // now that we have all the necessary objects for our user's identity
            // we can now sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return Ok(new {Message = "Logged in successfully"});
        }

        /*
            Logging out the user
        */
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // simply call the SightOutAsync method in the HttpContext object to sign out user.
            // this clear's the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // redirect the user to a route [like a homepage '/']
            return Ok(new { Message = "Logged out successfully" });
        }
    }
}
