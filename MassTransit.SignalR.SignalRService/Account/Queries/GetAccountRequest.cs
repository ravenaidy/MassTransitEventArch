using MediatR;

namespace MassTransit.SignalR.SignalRService.Account.Queries
{
    public class GetAccountRequest : IRequest<Models.Account>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}