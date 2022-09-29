using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Orchestrator.Events.Login
{
  public class AuthResponse : IEvent
  {
    public string Token { get; set; }
    public Guid CorrelationId { get; set; }
  }
}
