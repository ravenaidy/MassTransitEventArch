using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace MassTransit.BFFServices.SignalRWorker.Consumers
{
    public class GetAccountConsumer : IConsumer<Models.Account>
    {
        private readonly HubConnection _hubContext;

        public GetAccountConsumer(HubConnection hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }
        
        public async Task Consume(ConsumeContext<Models.Account> context)
        {
            await _hubContext.InvokeAsync("PublishAccount", context.Message);
        }
    }
}