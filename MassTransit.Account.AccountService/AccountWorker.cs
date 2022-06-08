namespace MassTransit.Account.AccountService
{
    public class AccountWorker : BackgroundService
    {
        private readonly ILogger<AccountWorker> _logger;

        public AccountWorker(ILogger<AccountWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("AccountWorker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}