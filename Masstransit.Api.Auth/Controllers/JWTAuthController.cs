using Masstransit.Api.Auth.DTO;
using Masstransit.Api.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Masstransit.Api.Auth.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly ILogger<JwtAuthController> _logger;
        private readonly ITokenService _tokenService;

        public JwtAuthController(ILogger<JwtAuthController> logger, ITokenService tokenService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [Route("gettoken")]
        public async Task<IActionResult> GetToken([FromBody] Login login)
        {
            var token = await _tokenService.GenerateToken(login);
            _logger.LogInformation($"This is the token:{token}");

            return Ok(token);
        }
    }

