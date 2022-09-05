using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.LoginService.Events
{
    public class LoginCreated : IEvent
    {
        public Guid LoginId { get; set; }
        public DateTime LoginCreatedTimeStamp { get; set; }
        
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}