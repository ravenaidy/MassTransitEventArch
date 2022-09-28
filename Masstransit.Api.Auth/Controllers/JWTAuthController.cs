using MassTransit;
using Masstransit.Api.Auth.DTO;
using Masstransit.Api.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Masstransit.Api.Auth.Controllers ;


[Route("api/[controller]")]
[ApiController]
public class JwtAuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IRequestClient<GetAuthToken> _client;

    public JwtAuthController(ITokenService tokenService, IRequestClient<GetAuthToken> client)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    [Route("gettoken")]
    public async Task<IActionResult> GetToken([FromBody] GetAuthToken authToken)
    {
        return Ok(await _tokenService.GenerateToken(authToken));
    }
    
    public async Task<IActionResult> RequestGetToken()
    {
        var request = await _client.GetResponse<GetAuthToken>(Request);
        return Ok(await _tokenService.GenerateToken(request.Message));
    }
}
