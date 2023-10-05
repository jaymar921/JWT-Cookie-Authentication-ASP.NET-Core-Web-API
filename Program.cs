using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;
using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Helpers;
using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Middlewares;
using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // lets add cors
            builder.Services.AddCors();

            // configure strongly typed settings object
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // configure dependency injection
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<Services.IAuthenticationService, Services.AuthenticationService>();

            // configure Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie( option => option.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = async (context) =>
                    {
                        // validates the cookie
                        await CookieHelper.ValidateCookie(context, builder.Configuration["AppSettings:SecretKey"]);
                    }
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // a middleware that will check for cookies if it exists then logs it for testing purpose
            app.UseMiddleware<CookieMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}