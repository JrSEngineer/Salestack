using Microsoft.IdentityModel.Tokens;
using Salestack.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Salestack.Services
{
    public class TokenService
    {
        public string GenerateToken(Authentication authentication)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string serverEncoderKey = Environment.GetEnvironmentVariable("SECURE_KEY") ?? "";

            var secretKey = Encoding.ASCII.GetBytes(serverEncoderKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.PrimarySid, authentication.UserId.ToString()),
                        new Claim(ClaimTypes.Role, authentication.Occupation.ToString()),
                        new Claim(ClaimTypes.Email, authentication.Email)
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken()
        {
            return true;
        }
    }
}
