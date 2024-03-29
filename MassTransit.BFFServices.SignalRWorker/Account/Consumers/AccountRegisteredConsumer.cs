﻿using System;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker.Account.Consumers
{
    public class AccountRegisteredConsumer : IConsumer<AccountRegistered>
    {
        private readonly ILogger<AccountRegisteredConsumer> _logger;
        private readonly HubConnection _hubContext;

        public AccountRegisteredConsumer(ILogger<AccountRegisteredConsumer> logger, HubConnection hubContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Consume(ConsumeContext<AccountRegistered> context)
        {
            _logger.LogPublishToHubInformation(nameof(BFFServices), nameof(AccountRegisteredConsumer), nameof(Consume),
                context.Message);
            await _hubContext.InvokeAsync("SendAccountCreated", context.Message);
        }
    }
}