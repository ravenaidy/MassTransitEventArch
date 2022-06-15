using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SignalRWorker.Consumers
{
    public class GetAccountConsumers : IConsumer<MassTransit.SignalR.SignalRService.Models.Account>
    {
        private readonly IHubContext<MassTransitAccountHub, IMassTransitAccountHub> _hubContext;

        public GetAccountConsumers(IHubContext<MassTransitAccountHub, IMassTransitAccountHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }
        
        public async Task Consume(ConsumeContext<MassTransit.SignalR.SignalRService.Models.Account> context)
        {
            await _hubContext.Clients.All.PublishAccount(context.Message);
        }
    }
}