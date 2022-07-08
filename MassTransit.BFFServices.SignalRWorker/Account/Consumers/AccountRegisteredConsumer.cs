using System;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using Microsoft.AspNetCore.SignalR.Client;

namespace MassTransit.BFFServices.SignalRWorker.Account.Consumers
{
    public class AccountRegisteredConsumer : IConsumer<AccountCreated>
    {
        private readonly HubConnection _hubContext;

        public AccountRegisteredConsumer(HubConnection hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Consume(ConsumeContext<AccountCreated> context)
        {
            await _hubContext.InvokeAsync("SendAccountCreated", JsonSerializer.Serialize(context.Message,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            ));
        }
    }
}
