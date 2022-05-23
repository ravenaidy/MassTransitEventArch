using System;

namespace MassTransit.AccountOrchestrator.Events
{
    public class LoginCreated
    {
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}