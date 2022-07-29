using System;

namespace MassTransit.SignalR.SignalRService.DTO
{
    public class AccountRegistered
    {
        public bool IsRegistered { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}