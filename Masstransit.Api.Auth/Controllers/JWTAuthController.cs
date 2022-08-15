using Masstransit.Api.Auth.DTO;
using Masstransit.Api.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Masstransit.Api.Auth.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public JwtAuthController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        public async Task<IActionResult> GetToken(Login login)
        {
            return Ok(await _tokenService.GenerateToken(login));
        }
    }

