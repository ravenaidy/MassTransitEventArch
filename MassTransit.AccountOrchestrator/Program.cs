using MassTransit.AccountOrchestrator;
using MassTransit;
using MassTransit.AccountOrchestrator.Dependencies;
using MassTransit.AccountOrchestrator.Events.Accounts;
using MassTransit.AccountOrchestrator.Events.Login;
using MassTransit.AccountOrchestrator.StateMachine.Account;
using MassTransit.AccountOrchestrator.StateMachine.Login;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((ctx, log) =>
    {
        log.ReadFrom.Configuration(ctx.Configuration);
    })
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;
        services.AddHostedService<Worker>();
        services.AddLoginStateMachineSettings(config);

        services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["RabbitMq:Config:Host"], massTransitHost => 
                    {
                        massTransitHost.Username(config["RabbitMq:Config:Username"]);
                        massTransitHost.Password(config["RabbitMq:Config:Password"]);
                    });
                
                    cfg.ConfigureEndpoints(context);
                });

                bus.AddRider(rider =>
                {
                    rider.AddSagaStateMachine<AccountStateMachine, AccountState>().InMemoryRepository();
                    rider.AddSagaStateMachine<LoginStateMachine, LoginState>().InMemoryRepository();

                    rider.AddProducer<CreateLogin>(config["Kafka:Config:CreateLoginTopic"]);
                    rider.AddProducer<CreateAccount>(config["Kafka:Config:CreateAccountTopic"]);
                    rider.AddProducer<AccountCreated>(config["Kafka:Config:AccountRegisteredTopic"]);
                    rider.AddProducer<LoginRequest>(config["Kafka:Config:GetLoginTopic"]);
                    rider.AddProducer<AuthLogin>(config["Kafka:Config:LoginAuthResponseTopic"]);

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

                            kafka.TopicEndpoint<AccountCreated>(config["Kafka:Config:AccountCreatedTopic"],
                                config["Kafka:Config:LoginGroup"],
                                c =>
                                {
                                    c.ConfigureSaga<AccountState>(context);
                                });

                            kafka.TopicEndpoint<LoginRequest>(config["Kafka:Config:LoginRequestTopic"],
                                config["Kafka:Config:LoginGroup"],
                                c =>
                                {
                                    c.ConfigureSaga<LoginState>(context);
                                });
                            kafka.TopicEndpoint<LoginResponse>(config["Kafka:Config:LoginResponseTopic"],
                                config["Kafka:Config:LoginGroup"],
                                c =>
                                {
                                    c.ConfigureSaga<LoginState>(context);
                                });
                        }
                    );
                });
            }
        );
    })
    .Build();

await host.RunAsync();