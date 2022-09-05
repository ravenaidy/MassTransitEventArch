using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.DTO
{
    public class LoginResponse : IEvent
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}