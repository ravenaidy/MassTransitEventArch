using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.BFFServices.SignalRWorker.Account.Commands;

public class AccountRegistered : IEvent
{
    public Guid LoginId { get; set; }
    public bool IsRegistered { get; set; }
    public DateTime CreatedTimeStamp { get; set; }
    
    [NotLogged]
    public Guid CorrelationId { get; set; }
}