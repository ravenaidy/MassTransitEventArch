using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRWorker.Account.Queries;

namespace SignalRWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private HubConnection? hubConnection;


        public Worker(ILogger<Worker> logger, IConfiguration configuration, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(_configuration["MassTransitHub:Config:Username"])
                .Build();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                hubConnection.On<GetAccountRequest>("ReceiveGetAccountRequest", (request) =>
                {
                    _mediator.Send(request, stoppingToken);
                });

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}