using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Orchestrator.Events.Login;

public class NoLogin : IEvent
{
  public Guid CorrelationId { get; set; }
}