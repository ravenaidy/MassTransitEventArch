namespace MassTransit.LoginService
{

    public class LoginWorker : BackgroundService
    {
        private readonly ILogger<LoginWorker> _logger;

        public LoginWorker(ILogger<LoginWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}