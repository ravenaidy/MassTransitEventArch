using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GetAccountRequest = MassTransit.BFFServices.SignalRWorker.Account.Queries.GetAccountRequest;

namespace MassTransit.BFFServices.SignalRWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _hubConnection;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, HubConnection hubConnection, IServiceScopeFactory scopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubConnection = hubConnection ?? throw new ArgumentNullException(nameof(hubConnection));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _hubConnection.StartAsync(stoppingToken);

            _hubConnection.On<GetAccountRequest>("PublishGetAccountRequest", (request) =>
            {
                //_mediator.Send(request, stoppingToken);
            });

            _hubConnection.On<string>("PublishNewAccountRequest", async request =>
            {
                var newRegistration = JsonSerializer.Deserialize<NewAccountRequest>(request,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (newRegistration is null) return;

                await ProcessSignalRMessage(newRegistration, stoppingToken);
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task ProcessSignalRMessage<T>(T message, CancellationToken stoppingToken) where T : notnull
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(message, stoppingToken);
        }
    }
}