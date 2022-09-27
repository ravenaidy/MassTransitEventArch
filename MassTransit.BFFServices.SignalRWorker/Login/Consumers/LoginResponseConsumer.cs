using System;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Consumers;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker.Login.Consumers
{
    public class LoginResponseConsumer : IConsumer<Models.Login>
    {
        private readonly ILogger<LoginResponseConsumer> _logger;
        private readonly HubConnection _hubContext;

        public LoginResponseConsumer(ILogger<LoginResponseConsumer> logger, HubConnection hubContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }
        
        public async Task Consume(ConsumeContext<Models.Login> context)
        {
            _logger.LogPublishToHubInformation(nameof(BFFServices), nameof(AccountRegisteredConsumer), nameof(Consume),
                context.Message);
            await _hubContext.InvokeAsync("SendLogin", context.Message);
        }
    }
}