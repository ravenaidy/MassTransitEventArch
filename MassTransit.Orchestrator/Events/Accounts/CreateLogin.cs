using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Orchestrator.Events.Accounts;

public class CreateLogin : IEvent
{
    public Guid LoginId { get; set; }
    public string Username { get; set; }
    [NotLogged]
    public string Password { get; set; }
    [NotLogged]
    public Guid CorrelationId { get; set; }
}