using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.BFFServices.SignalRWorker.Models
{
    public class Login : IEvent
    {
        public int LoginId { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
        public Guid CorrelationId { get; set; }
    }
}