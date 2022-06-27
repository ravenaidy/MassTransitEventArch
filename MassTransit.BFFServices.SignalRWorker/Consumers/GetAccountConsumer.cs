using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.BFFServices.SignalRWorker.Consumers
{
    public class GetAccountConsumer : IConsumer<Models.Account>
    {
        private readonly IHubContext<MassTransitAccountHub, IMassTransitAccountHub> _hubContext;
        private readonly IMapper _mapper;

        public GetAccountConsumer(IHubContext<MassTransitAccountHub, IMassTransitAccountHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task Consume(ConsumeContext<Models.Account> context)
        {
            await _hubContext.Clients.All.PublishAccount(
                _mapper.Map<MassTransit.SignalR.SignalRService.Models.Account>(context.Message));
        }
    }
}