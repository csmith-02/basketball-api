using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FullCourtInsights.Auth
{
    public class AuthService(IOptions<JwtSettings> jwtSettings)
    {

        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public (string, JwtSecurityToken, string) GenerateToken(string email, string name)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, email),
                new("name", name)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var refreshToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), refreshToken, new JwtSecurityTokenHandler().WriteToken(refreshToken));
        }
    }
}
