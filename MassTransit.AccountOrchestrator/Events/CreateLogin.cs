using System;

namespace MassTransit.AccountOrchestrator.Events
{
    public class CreateLogin : CorrelatedBy<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid CorrelationId { get; }
    }
}