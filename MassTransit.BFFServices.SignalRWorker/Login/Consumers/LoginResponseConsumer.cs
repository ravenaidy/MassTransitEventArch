using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace MassTransit.BFFServices.SignalRWorker.Login.Consumers
{
    public class LoginResponseConsumer : IConsumer<Models.Login>
    {
        private readonly HubConnection _hubContext;

        public LoginResponseConsumer(HubConnection hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }
        
        public async Task Consume(ConsumeContext<Models.Login> context)
        {
            await _hubContext.InvokeAsync("SendLogin", context.Message);
        }
    }
}