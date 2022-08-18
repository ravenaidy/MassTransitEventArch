using System;

namespace MassTransit.AccountOrchestrator.Events.Login
{
    public class LoginRequest : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
