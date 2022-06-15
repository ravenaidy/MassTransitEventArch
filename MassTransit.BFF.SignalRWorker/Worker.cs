using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRWorker.Account.Queries;

namespace SignalRWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
        private readonly HubConnection _hubConnection;
        
        public Worker(HubConnection connection, ILogger<Worker> logger, IMediator mediator)
        {
            _hubConnection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _hubConnection.On<GetAccountRequest>("ReceiveGetAccountRequest", (request) =>
                {
                    _mediator.Send(request, stoppingToken);
                });

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}