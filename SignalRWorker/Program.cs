using SignalRWorker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
