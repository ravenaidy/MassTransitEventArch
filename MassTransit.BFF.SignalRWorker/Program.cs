using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRWorker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        services.AddHostedService<Worker>();
        
        services.AddSingleton(sp => new HubConnectionBuilder()
            .WithUrl(config["MassTransitHub:Url"])
            .WithAutomaticReconnect()
            .Build());

    })
    .Build();

await host.RunAsync();
