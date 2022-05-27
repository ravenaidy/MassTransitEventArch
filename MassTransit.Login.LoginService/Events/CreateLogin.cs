using MassTransit.LoginService.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;

namespace MassTransit.LoginService.Events
{
    public class CreateLogin : CorrelatedBy<Guid>, IMapTo<Login>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid CorrelationId { get; }
    }
}