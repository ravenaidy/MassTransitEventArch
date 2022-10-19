using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.Events
{
    public class LoginResponse : IEvent
    {
        public Guid LoginId { get; init; }
        public string Username { get; init; }
        public string Token { get; set; }
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}