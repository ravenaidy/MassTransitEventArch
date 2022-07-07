using System;

namespace MassTransit.BFFServices.SignalRWorker.Account.Commands
{
    public class AccountCreated
    {
        public bool IsCreated { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
