using Confluent.Kafka;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRWorker;
using SignalRWorker.Account.Queries;
using SignalRWorker.Consumers;
using SignalRWorker.Models;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        services.AddHostedService<Worker>();

        services.AddMassTransit(bus =>
        {
            bus.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config["RabbitMq:Config:Host"], rabbitHost =>
                {
                    rabbitHost.Username(config["RabbitMq:Config:Username"]);
                    rabbitHost.Password(config["RabbitMq:Config:Password"]);
                });

                cfg.ConfigureEndpoints(context);
            });

            bus.AddRider(rider =>
            {
                // Producers
                rider.AddProducer<GetAccountRequest>(config["Kafka:Config:GetLoginTopic"]);

                // Consumers
                rider.AddConsumer<GetAccountConsumer>();
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<Account>(config["Kafka:Config:CreateLoginTopic"], config["Kafka:Config:GetAccountTopic"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<GetAccountConsumer>(context);
                    });
                });

            });
        });

        services.AddMediatR(typeof(GetAccountRequest));
        
        services.AddSingleton(sp => new HubConnectionBuilder()
            .WithUrl(config["MassTransitHub:Url"])
            .WithAutomaticReconnect()
            .Build());

    })
    .Build();

await host.RunAsync();
