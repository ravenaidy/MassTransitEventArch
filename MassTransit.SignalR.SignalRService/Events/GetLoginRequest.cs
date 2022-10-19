using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.DTO
{
    public class GetLoginRequest : IEvent
    {
        public string Username { get; set; }
        [NotLogged]
        public string Password { get; set; }
        [NotLogged]
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}