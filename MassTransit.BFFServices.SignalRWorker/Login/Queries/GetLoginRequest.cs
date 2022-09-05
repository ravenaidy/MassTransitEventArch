using System;
using MassTransit.Shared.Infrastructure.Events;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Login.Queries
{
    public class GetLoginRequest : IRequest, IEvent
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public Guid CorrelationId { get; set; } 
    }
}