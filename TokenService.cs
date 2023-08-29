using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace authorization
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));


            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, user.Name)
            };

            var token = new JwtSecurityToken(null, null, claims, null, DateTime.Now.AddHours(1), new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

            string tokenvalue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenvalue;
        }
    }
}
