using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.BFFServices.SignalRWorker.Login.Events;

public class NoLogin : IEvent
{
  public Guid CorrelationId { get; set; }
}