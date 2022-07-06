using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GetAccountRequest = MassTransit.BFFServices.SignalRWorker.Account.Queries.GetAccountRequest;

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
            await _hubConnection.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                _hubConnection.On<GetAccountRequest>("PublishGetAccountRequest", (request) =>
                {
                    _mediator.Send(request, stoppingToken);
                });

                _hubConnection.On<NewAccountRequest>("PublishNewAccountRequest", request =>
                {
                    _mediator.Send(request, stoppingToken);
                });
                
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}