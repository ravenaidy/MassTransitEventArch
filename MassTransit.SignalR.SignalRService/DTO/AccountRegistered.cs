using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.DTO
{
    public class AccountRegistered : IEvent
    {
        public bool IsRegistered { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}