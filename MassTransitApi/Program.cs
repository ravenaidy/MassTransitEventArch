using MassTransit;
using MassTransitApi.Dto;
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
    bus.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
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