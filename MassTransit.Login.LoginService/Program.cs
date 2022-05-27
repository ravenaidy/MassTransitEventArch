using System.Data.SqlClient;
using Confluent.Kafka;
using MassTransit.LoginService;
using MassTransit;
using MassTransit.LoginService.Consumers;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions;
using MassTransit.Shared.Infrastructure.DBConnection;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;

        // Add mappings
        services.AddObjectMapping(
            typeof(Login).Assembly);

        // Add Services & Repositories
        services.AddSingleton<IConnectionFactory>(sp =>
            new ConnectionFactory<SqlConnection>(config.GetConnectionString("MassTransitDB")));
        services.AddSingleton<ILoginRepository, LoginRepository>();

        services.AddHostedService<LoginWorker>();
        services.AddMassTransit(bus =>
        {
            bus.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

            bus.AddRider(rider =>
            {
                // Producers
                rider.AddProducer<LoginCreated>(config["Kafka:Config:LoginCreatedTopic"]);

                // Consumers
                rider.AddConsumer<CreateLoginConsumer>();
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<CreateLogin>(config["Kafka:Config:CreateLoginTopic"], config["Kafka:Config:LoginGroup"], c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<CreateLoginConsumer>(context);
                    });
                });

            });
        });
    })
    .Build();

await host.RunAsync();