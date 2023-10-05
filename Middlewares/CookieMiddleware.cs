using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the current endpoint has [Authorize] attribute
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var authorizeAttribute = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();
                if (authorizeAttribute != null)
                {
                    // [Authorize] attribute is present on the endpoint
                    // You can perform your cookie check here
                    if (context.Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
                    {
                        var cookieValue = context.Request.Cookies[".AspNetCore.Cookies"];

                        // for testing purpose, print the value of the cookie
                        Console.WriteLine($"Found the cookie with value: {cookieValue}");
                    }
                }
            }

            // Continue processing the request pipeline
            await _next(context);
        }
    }
}
