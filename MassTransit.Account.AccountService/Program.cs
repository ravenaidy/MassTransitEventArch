using System.Data.SqlClient;
using Confluent.Kafka;
using MassTransit;
using MassTransit.Account.AccountService;
using MassTransit.Account.AccountService.Consumers;
using MassTransit.Account.AccountService.Events;
using MassTransit.Account.AccountService.Models;
using MassTransit.Account.AccountService.Repositories;
using MassTransit.Account.AccountService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions;
using MassTransit.Shared.Infrastructure.DBConnection;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host,services) =>
    {
        var config = host.Configuration;

        // Add mappings
        services.AddObjectMapping(
            typeof(Account).Assembly);

        // Add Services & Repositories
        services.AddSingleton<IConnectionFactory>(sp =>
            new ConnectionFactory<SqlConnection>(config.GetConnectionString("MassTransitAccountDB")));
        services.AddSingleton<IAccountRepository, AccountRepository>();

        services.AddHostedService<AccountWorker>();
        
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
                rider.AddProducer<AccountCreated>(config["Kafka:Config:AccountCreatedTopic"]);

                // Consumers
                rider.AddConsumer<CreateAccountConsumer>();
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host(config["Kafka:Config:Host"]);

                    kafka.TopicEndpoint<CreateAccount>(config["Kafka:Config:CreateAccountTopic"], config["Kafka:Config:AccountGroup"], c =>
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