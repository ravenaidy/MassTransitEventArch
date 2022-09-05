using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Account.AccountService.Events;

public class AccountCreated : IEvent
{
    public Guid AccountId { get; set; }
    public DateTime CreatedTimeStamp { get; set; }
    
    [NotLogged]
    public Guid CorrelationId { get; set; }
}