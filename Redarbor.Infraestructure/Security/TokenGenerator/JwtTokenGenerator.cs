using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Redarbor.Infraestructure.Security.TokenGenerator
{
    internal class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
    {
        public string GenerateToken(Guid id, string name)
        {
            JwtSettings jwtSettings = GetOptions();

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Name, name),
                new("id", id.ToString()),
            ];

            JwtSecurityToken token = new(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSettings GetOptions()
        {
            return new JwtSettings
            {
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"],
                Secret = configuration["Jwt:Key"]
            };
        }
    }
}