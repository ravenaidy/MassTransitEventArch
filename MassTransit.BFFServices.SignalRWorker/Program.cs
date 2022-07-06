using System.Reflection;
using Confluent.Kafka;
using MassTransit;
using MassTransit.BFFServices.SignalRWorker;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MassTransit.BFFServices.SignalRWorker.Account.Handlers;
using MassTransit.BFFServices.SignalRWorker.Account.Queries;
using MassTransit.BFFServices.SignalRWorker.Consumers;
using MassTransit.BFFServices.SignalRWorker.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        
        // Add mappings
        services.AddObjectMapping(
            typeof(Account).Assembly);
        
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
                rider.AddProducer<NewAccountRequest>(config["Kafka:Config:RegisterAccountTopic"]);

                // Consumers
                rider.AddConsumer<GetAccountConsumer>();
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<Account>(config["Kafka:Config:GetAccountTopic"], config["Kafka:Config:SignalRGroup"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<GetAccountConsumer>(context);
                    });
                });
            });
        });
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton(sp => new HubConnectionBuilder()
            .WithUrl(config["MassTransitHub:Url"])  
            .WithAutomaticReconnect()
            .Build());
    })
    .Build();

await host.RunAsync();