using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
        private readonly HubConnection _hubConnection;

        public Worker(ILogger<Worker> logger, IMediator mediator, HubConnection hubConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _hubConnection = hubConnection ?? throw new ArgumentNullException(nameof(hubConnection));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _hubConnection.On<string>("ReceiveGetAccountRequest", (request) =>
                {
                    var getAccountRequest = JsonSerializer.Deserialize<GetAccountRequest>(request);
                    
                    if (getAccountRequest is not null)
                        _mediator.Send( getAccountRequest, stoppingToken);
                });
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}