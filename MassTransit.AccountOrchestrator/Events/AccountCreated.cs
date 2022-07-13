using System;

namespace MassTransit.AccountOrchestrator.Events
{
    public class AccountCreated : CorrelatedBy<Guid>
    {
        public bool IsRegistered { get; init; }
        public DateTime CreatedTimeStamp { get; init; }
        public Guid CorrelationId { get; }
    }
}