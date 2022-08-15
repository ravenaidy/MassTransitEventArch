using System;

namespace MassTransit.AccountOrchestrator.Events.Login
{
    public class LoginResponse : CorrelatedBy<Guid>
    {
        public int UserId { get; set; }
        public Guid CorrelationId { get; }
    }
}
