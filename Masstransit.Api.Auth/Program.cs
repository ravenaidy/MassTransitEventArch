using Confluent.Kafka;
using MassTransit;
using Masstransit.Api.Auth.Consumers;
using Masstransit.Api.Auth.Events;
using Masstransit.Api.Auth.Helpers;
using Masstransit.Api.Auth.Services;
using Masstransit.Api.Auth.Services.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ITokenService, TokenService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TokenConfiguration(builder.Configuration);



var config = builder.Configuration;
var services = builder.Services;

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
    rider.AddProducer<AuthTokenResponse>(config["Kafka:Config:TokenResponseTopic"]);

    // Consumers
    rider.AddConsumer<GetAuthTokenConsumer>();

    rider.UsingKafka((context, kafka) =>
    {
      kafka.Host(config["Kafka:Config:Host"]);

      kafka.TopicEndpoint<GetAuthToken>(config["Kafka:Config:TokenRequestTopic"], config["Kafka:Config:AuthGroup"],
        c =>
        {
          c.AutoOffsetReset = AutoOffsetReset.Earliest;
          c.ConfigureConsumer<GetAuthTokenConsumer>(context);
        });
    });
  });
});

var logger = new LoggerConfiguration()
  .ReadFrom
  .Configuration(builder.Configuration)
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();