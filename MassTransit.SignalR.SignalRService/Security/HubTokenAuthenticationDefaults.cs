using System;
using Microsoft.AspNetCore.Authentication;

namespace MassTransit.SignalR.SignalRService.Security;

public static class HubTokenAuthenticationDefaults
{
  private const string AuthenticationScheme = "HubTokenAuthentication";
  public static AuthenticationBuilder AddHubTokenAuthenticationScheme(this AuthenticationBuilder builder)
  {
    return AddHubTokenAuthenticationScheme(builder, options => { });
  }

  public static AuthenticationBuilder AddHubTokenAuthenticationScheme(this AuthenticationBuilder builder, Action<HubTokenAuthenticationOptions> configureOptions)
  {
    return builder.AddScheme<HubTokenAuthenticationOptions, HubTokenAuthenticationHandler>(AuthenticationScheme, configureOptions);
  }
}