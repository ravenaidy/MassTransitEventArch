using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.Events;

public class NoLogin : IEvent
{
  public Guid CorrelationId { get; set; }
}