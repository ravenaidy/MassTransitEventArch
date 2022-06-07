using System;

namespace MassTransit.Account.AccountService.Events
{
    public class AccountCreated : CorrelatedBy<Guid>
    {
        public DateTime CreatedTimeStamp { get; set; }
        public Guid CorrelationId { get; }
    }
}