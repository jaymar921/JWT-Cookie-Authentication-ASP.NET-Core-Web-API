using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Helpers
{
    public class CookieHelper
    {

        public static async Task ValidateCookie(CookieValidatePrincipalContext context, string SecretKey)
        {
            var claimsPrincipal = context.Principal;
            if (claimsPrincipal != null)
            {
                // get the tokens
                var claimToken = claimsPrincipal.Claims.First(c => c.Type == "token").Value;

                if (claimToken != null)
                {
                    User? user = JWTHelper.DecodeJsonWebTokenToUser(claimToken, SecretKey);

                    if (user == null)
                    {
                        Console.WriteLine("Cookie has Expired");
                        context.RejectPrincipal();
                        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                    else
                    {
                        Console.WriteLine(user.ToString());
                    }
                }
            }
        }
    }
}
