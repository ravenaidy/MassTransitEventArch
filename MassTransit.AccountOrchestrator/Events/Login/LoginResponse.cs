using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.AccountOrchestrator.Events.Login
{
    public class LoginResponse : IEvent
    {
        public Guid LoginId { get; set; }
        public string Username { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
