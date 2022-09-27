using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.AccountOrchestrator.Events.Accounts;

public class AccountCreated : IEvent
{
    public Guid LoginId { get; set; }

    public bool IsRegistered { get; init; }
    public DateTime CreatedTimeStamp { get; init; }
    [NotLogged]
    public Guid CorrelationId { get; set; }
}