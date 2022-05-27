using MassTransit.AccountOrchestrator;
using MassTransit.AccountOrchestrator.Events;
using MassTransit.AccountOrchestrator.StateMachine;
using MassTransit;

var host = Host.CreateDefaultBuilder(args)
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
                                    c.ConfigureSaga<AccountState>(context);
                                });
                        }
                    );
                });
            }
        );
    })
    .Build();

await host.RunAsync();