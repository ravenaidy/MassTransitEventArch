using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

const string corsPolicy = "defaultPolicy";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsPolicy,
        policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost")
            .AllowCredentials());
});

var app = builder.Build();

app.UseCors(corsPolicy);

app.MapHub<MassTransitAccountHub>("/masstransitHub");

app.Run();