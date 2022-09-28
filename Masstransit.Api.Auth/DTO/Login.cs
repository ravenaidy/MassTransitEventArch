using MassTransit.Shared.Infrastructure.Events;

namespace Masstransit.Api.Auth.DTO
{
    public class GetAuthToken : IEvent
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public Guid CorrelationId { get; set; }
    }
}