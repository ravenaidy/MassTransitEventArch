using System;

namespace MassTransit.Orchestrator.Events.Login;

public class NoLogin
{
  public Guid CorrelationId { get; set; }
}