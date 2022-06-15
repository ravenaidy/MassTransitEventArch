using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace SignalRWorker.Consumers
{
    public class GetAccountConsumer : IConsumer<Models.Account>
    {
        private readonly IHubContext _hubContext;

        public GetAccountConsumer(IHubContext hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }
        
        public async Task Consume(ConsumeContext<Models.Account> context)
        {
            await _hubContext.Clients.All.SendAsync("SendAccount", context.Message);
        }
    }
}