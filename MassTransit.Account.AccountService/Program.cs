using Confluent.Kafka;
using MassTransit;
using MassTransit.Account.AccountService;
using MassTransit.Account.AccountService.Consumers;
using MassTransit.Account.AccountService.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host,services) =>
    {
        var config = host.Configuration;
        services.AddHostedService<Worker>();
        
        services.AddMassTransit(bus =>
        {
            bus.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config["RabbitMq:Config:Host"], host => 
                {
                    host.Username(config["RabbitMq:Config:Username"]);
                    host.Password(config["RabbitMq:Config:Password"]);
                });
                
                cfg.ConfigureEndpoints(context);
            });

            bus.AddRider(rider =>
            {
                // Producers
                rider.AddProducer<AccountCreated>(config["Kafka:Config:AccountCreatedTopic"]);

                // Consumers
                rider.AddConsumer<CreateAccountConsumer>();
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<CreateAccount>(config["Kafka:Config:CreateAccountTopic"], config["Kafka:Config:LoginGroup"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<CreateAccountConsumer>(context);
                    });
                });

            });
        });
    })
    .Build();

await host.RunAsync();