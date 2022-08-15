using System;

namespace MassTransit.AccountOrchestrator.Events.Accounts
{
    public class LoginCreated : CorrelatedBy<Guid>
    {
        public int LoginId { get; set; }
        public DateTime LoginCreatedTimeStamp { get; set; }
        public Guid CorrelationId { get; }
    }
}