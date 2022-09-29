using MassTransit.Shared.Infrastructure.Events;

namespace Masstransit.Api.Auth.Events
{
  public class AuthTokenResponse : IEvent
  {
    public string Token { get; set; }
    public Guid CorrelationId { get; set; }
  }
}
