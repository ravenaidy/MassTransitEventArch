using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MassTransit.Account.AccountService
{
    public class AccountWorker : BackgroundService
    { 
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}