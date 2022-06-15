using MediatR;

namespace SignalRWorker.Account.Queries
{
    public class GetAccountRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
