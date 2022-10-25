using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MassTransit.SignalR.SignalRService.Security;

public class HubTokenAuthenticationHandler : AuthenticationHandler<HubTokenAuthenticationOptions>
{
  private readonly IServiceProvider _serviceProvider;
  private readonly IConfiguration _configuration;
  public HubTokenAuthenticationHandler(IOptionsMonitor<HubTokenAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock, 
    IServiceProvider serviceProvider, 
    IConfiguration configuration) : base(options, logger, encoder, clock)
  {
    _serviceProvider = serviceProvider;
    _configuration = configuration;
  }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    var key = Encoding.ASCII.GetBytes( _configuration["JWTKey"]);
    var token = Request.Query["access_token"];
    if (string.IsNullOrEmpty(token))
    {
      return Task.FromResult(AuthenticateResult.Fail("No token were provided"));
    }
    
    var validatedToken = GetValidatedToken(token, key);

    if (validatedToken is null)
    {
      return Task.FromResult(AuthenticateResult.Fail("Token "));
    }
    
    var ticket = GenerateAuthenticationTicket(validatedToken);

    return Task.FromResult(AuthenticateResult.Success(ticket));
  }

  private AuthenticationTicket GenerateAuthenticationTicket(JwtSecurityToken validatedToken)
  {
    var claims = new[]
    {
      new Claim("id", validatedToken.Claims.First(claim => claim.Type == "userid").Value),
      new Claim("username", validatedToken.Claims.First(claim => claim.Type == "username").Value)
    };

    var identity = new ClaimsIdentity(claims, nameof(HubTokenAuthenticationHandler));
    var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    return ticket;
  }

  private JwtSecurityToken GetValidatedToken(string token, byte[] key)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out var validatedToken);

      return(JwtSecurityToken) validatedToken;
    }
    catch (Exception e)
    {
      return null;
    }
  }
}