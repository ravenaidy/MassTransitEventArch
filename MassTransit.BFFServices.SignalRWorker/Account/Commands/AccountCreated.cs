using System;

namespace MassTransit.BFFServices.SignalRWorker.Account.Commands
{
    public class AccountRegistered
    {
        public bool IsRegistered { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
