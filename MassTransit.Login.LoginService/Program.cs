using MassTransit.LoginService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.LoginService.Consumers;
using MassTransit.LoginService.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<LoginWorker>();
        services.AddMassTransit(bus =>
        {
            bus.AddRider(rider =>
            {
                rider.AddConsumer<CreateLoginConsumer>();

                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host("localhost:9092");

                    kafka.TopicEndpoint<CreateLogin>("masstransitarch-login-createlogin", "logingroup", config =>
                    {
                        config.ConfigureConsumer<CreateLoginConsumer>(context);
                    });
                });
            });
        });
    })
    .Build();

await host.RunAsync();