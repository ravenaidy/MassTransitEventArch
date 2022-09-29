using MassTransit.Shared.Infrastructure.Events;

namespace Masstransit.Api.Auth.Events
{
    public class GetAuthToken : IEvent
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid CorrelationId { get; set; }
    }
}