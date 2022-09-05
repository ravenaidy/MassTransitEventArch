using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.LoginService.Events
{
    public class GetLogin : IEvent
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        [NotLogged]
        public Guid CorrelationId { get; set; }
    }
}