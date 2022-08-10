using MassTransit.Chat.MessagesService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<MessagesWorker>(); })
    .Build();

await host.RunAsync();