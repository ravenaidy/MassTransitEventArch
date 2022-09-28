using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.AccountOrchestrator.Events.Login
{
    public class LoginRequest : IEvent
    {
        public Guid CorrelationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
