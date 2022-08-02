using MassTransit.BFFServices.SignalRWorker.Models;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Queries
{
    public class GetLoginRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}