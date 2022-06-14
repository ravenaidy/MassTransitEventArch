using System;

namespace MassTransit.AccountOrchestrator.Events
{
    public record AccountCreated(Guid CorrelationId) : CorrelatedBy<Guid>
    {
        public DateTime CreatedTimeStamp { get; set; }
    }
}