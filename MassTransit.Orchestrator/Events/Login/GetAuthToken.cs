using System;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.AccountOrchestrator.Events.Login;

    public class GetAuthToken : IEvent
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid CorrelationId { get; set; }
    }

