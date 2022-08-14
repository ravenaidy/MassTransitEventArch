using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Masstransit.Api.Auth.DTO;
using Masstransit.Api.Auth.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Masstransit.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private readonly JWTConfiguration _jwtConfiguration;

        public JWTAuthController(JWTConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration ?? throw new ArgumentNullException(nameof(jwtConfiguration));
        }
        public Task<IActionResult> GetToken(Login login)
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
            return OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
