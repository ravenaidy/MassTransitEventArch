using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Security;

public class HubRequirement : AuthorizationHandler<HubRequirement, HubInvocationContext>, IAuthorizationRequirement
{
  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HubRequirement requirement,
    HubInvocationContext resource)
  {
    
    context.Succeed(requirement);
    return Task.CompletedTask;
  }
}