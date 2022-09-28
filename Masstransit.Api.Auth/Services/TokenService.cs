using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Masstransit.Api.Auth.DTO;
using Masstransit.Api.Auth.Helpers;
using Masstransit.Api.Auth.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Masstransit.Api.Auth.Services
{

    public class TokenService : ITokenService
    {
        private readonly JWTConfiguration _jwtConfiguration;

        public TokenService(JWTConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration ?? throw new ArgumentNullException(nameof(jwtConfiguration));
        }

        public Task<string> GenerateToken(GetAuthToken login)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtConfiguration.Subject),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat,
                    DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim("UserId", login.UserId.ToString()),
                new Claim("Username", login.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_jwtConfiguration.Issuer,
                _jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);

            return Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

