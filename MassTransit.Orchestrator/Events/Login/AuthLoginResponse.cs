﻿using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Orchestrator.Events.Login
{
  public class AuthLoginResponse : IEvent
  {
    public Guid LoginId { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
    public Guid CorrelationId { get; set; }
  }
}
