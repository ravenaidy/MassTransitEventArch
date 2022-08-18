using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Login.Queries
{
    public class GetLoginRequest : IRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}