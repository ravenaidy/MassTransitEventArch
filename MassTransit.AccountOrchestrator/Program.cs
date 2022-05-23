using MassTransit.AccountOrchestrator;
using MassTransit.AccountOrchestrator.Events;
using MassTransit.AccountOrchestrator.StateMachine;
using Confluent.Kafka;
using MassTransit;
using MassTransit.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        services.AddHostedService<Worker>();

        services.AddMassTransit(bus =>
            {
                bus.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
                bus.AddRider(rider =>
                {
                    rider.AddSagaStateMachine<AccountStateMachine, AccountState>().InMemoryRepository();
                    rider.AddProducer<CreateLogin>(config["Kafka:Config:CreateLoginTopic"]);

                    rider.UsingKafka((context, kafka) =>
                        {
                            kafka.Host(config["Kafka:Config:Host"]);

                            kafka.TopicEndpoint<RegisterAccount>(config["Kafka:Config:RegisterAccountTopic"],
                                config["Kafka:Config:LoginGroup"],
                                c =>
                                {
                                    c.AutoOffsetReset = AutoOffsetReset.Earliest;
                                    c.ConfigureSaga<AccountState>(context);
                                });
                            kafka.TopicEndpoint<LoginCreated>(config["Kafka:Config:LoginCreatedTopic"],
                                config["Kafka:Config:LoginGroup"], c =>
                                {
                                    c.AutoOffsetReset = AutoOffsetReset.Earliest;
                                    c.ConfigureSaga<AccountState>(context);
                                });
                        }
                    );
                });
            }
        );
    })
    .Build();

var bus = host.Services.GetService<IBusControl>();
var busHandle = TaskUtil.Await(() => bus.StartAsync());

await host.RunAsync();