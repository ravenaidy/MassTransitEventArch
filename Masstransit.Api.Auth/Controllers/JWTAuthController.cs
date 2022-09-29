using Masstransit.Api.Auth.Events;
using Masstransit.Api.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Masstransit.Api.Auth.Controllers ;

[Route("api/[controller]")]
[ApiController]
public class JwtAuthController : ControllerBase
{
  private readonly ITokenService _tokenService;

  public JwtAuthController(ITokenService tokenService)
  {
    _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
  }

  [Route("gettoken")]
  public async Task<IActionResult> GetToken([FromBody] GetAuthToken authToken)
  {
    return Ok(await _tokenService.GenerateToken(authToken));
  }
}
