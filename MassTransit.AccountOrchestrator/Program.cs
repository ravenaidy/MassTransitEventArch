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
                    rider.AddSagaStateMachine<AccountStateMachine, AccountState>().InMemoryRepository();

                    rider.AddProducer<CreateLogin>(config["Kafka:Config:CreateLoginTopic"]);
                    rider.AddProducer<CreateAccount>(config["Kafka:Config:CreateAccountTopic"]);

                    rider.UsingKafka((context, kafka) =>
                        {
                            kafka.Host(config["Kafka:Config:Host"]);

                            kafka.TopicEndpoint<RegisterAccount>(config["Kafka:Config:RegisterAccountTopic"],
                                config["Kafka:Config:LoginGroup"],
                                c =>
                                {
                                    c.ConfigureSaga<AccountState>(context);
                                });
                            kafka.TopicEndpoint<LoginCreated>(config["Kafka:Config:LoginCreatedTopic"],
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