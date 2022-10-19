using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.BFFServices.SignalRWorker.Models
{
    public class Login : IEvent
    {
        public Guid LoginId { get; init; }

        public string Username { get; init; }

        public string Token { get; init; }
        public Guid CorrelationId { get; set; }
    }
}