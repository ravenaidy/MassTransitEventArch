using System.Reflection;
using Confluent.Kafka;
using MassTransit;
using MassTransit.BFFServices.SignalRWorker;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MassTransit.BFFServices.SignalRWorker.Account.Consumers;
using MassTransit.BFFServices.SignalRWorker.Login.Consumers;
using MassTransit.BFFServices.SignalRWorker.Login.Queries;
using MassTransit.BFFServices.SignalRWorker.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        
        // Add mappings
        services.AddObjectMapping(
            typeof(Login).Assembly);
        
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
                rider.AddProducer<GetLoginRequest>(config["Kafka:Config:LoginRequestTopic"]);
                rider.AddProducer<NewAccountRequest>(config["Kafka:Config:RegisterAccountTopic"]);

                // Consumers
                rider.AddConsumer<LoginResponseConsumer>();
                rider.AddConsumer<AccountRegisteredConsumer>();

                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<Login>(config["Kafka:Config:LoginAuthResponseTopic"], config["Kafka:Config:SignalRGroup"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<LoginResponseConsumer>(context);
                    });
                    kafka.TopicEndpoint<AccountRegistered>(config["Kafka:Config:AccountRegisteredTopic"], config["Kafka:Config:SignalRGroup"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<AccountRegisteredConsumer>(context);
                    });
                });
            });
        });
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton(sp => new HubConnectionBuilder()
            .WithUrl(config["MassTransitHub:Url"])  
            .WithAutomaticReconnect()
            .AddJsonProtocol(opt => opt.PayloadSerializerOptions.PropertyNameCaseInsensitive = true)
            .Build());
    })
    .Build();

await host.RunAsync();