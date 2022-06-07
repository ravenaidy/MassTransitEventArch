using MassTransit;
using MassTransitApi.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "MassTransitAPI", Version = "v1"})
);

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(config["RabbitMq:Config:Host"], "/",host => 
        {
            host.Username(config["RabbitMq:Config:Username"]);
            host.Password(config["RabbitMq:Config:Password"]);
        });
                
        cfg.ConfigureEndpoints(context);
    });

    bus.AddRider(rider =>
    {
        rider.AddProducer<Account>(config["Kafka:Config:RegisterAccountTopic"]);

        rider.UsingKafka((context, kafka) =>
        {
            kafka.Host(config["Kafka:Config:Host"]);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();