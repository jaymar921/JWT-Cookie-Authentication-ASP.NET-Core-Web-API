using JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Cookie_Authentication_ASP.NET_Core_Web_API.Helpers
{
    public class JWTHelper
    {
        /*
         * Helper method, for generating Json Web Token
         * 
         * install Nuget System.IdentityModel.Tokens.Jwt
         */
        public static string GenerateJsonWebToken(User user, AppSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // makes the properties of the user to be the claim identity, parding user into the token
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("email", user.Email),
                    new Claim("username", user.Username),
                    new Claim("favoritecolor", user.FavoriteColor),
                }),

                // Set the token expiry to a day
                Expires = DateTime.Now.AddMinutes(settings.IntExpireHour),

                // setting the signing credentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            // create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static User? DecodeJsonWebTokenToUser(string jsonWebToken, string SecretKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                tokenHandler.ValidateToken(jsonWebToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // set clockskew to zero so token expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                
                // Logging Purpose
                Console.WriteLine("Cookie was issued at "+jwtToken.IssuedAt);
                Console.WriteLine("Cookie was valid to " + jwtToken.ValidTo);

                int id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                string name = jwtToken.Claims.First(x => x.Type == "name").Value;
                string email = jwtToken.Claims.First(x => x.Type == "email").Value;
                string username = jwtToken.Claims.First(x => x.Type == "username").Value;
                string favoritecolor = jwtToken.Claims.First(x => x.Type == "favoritecolor").Value;

                // Return the decoded user from the token
                return new User
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    Username = username,
                    FavoriteColor = favoritecolor
                };
            }
            catch
            {
                return null;
            }
        } 
    }
}
