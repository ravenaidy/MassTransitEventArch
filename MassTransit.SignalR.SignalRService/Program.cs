using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

const string corsPolicy = "defaultPolicy";

var configuration = builder.Configuration;

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsPolicy,
        policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(configuration.GetValue<string>("CorsUrl"))
            .AllowCredentials());
});

var app = builder.Build();

app.UseCors(corsPolicy);

app.MapHub<MassTransitAccountHub>("/masstransitHub");
app.MapHub<MassTransitAccountHub>("/masstransitChatHub");

app.Run();