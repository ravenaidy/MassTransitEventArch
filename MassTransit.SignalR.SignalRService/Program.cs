using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, log) =>
    log.ReadFrom.Configuration(ctx.Configuration));

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

app.UseSerilogRequestLogging();
app.UseCors(corsPolicy);

app.MapHub<MassTransitAccountHub>("/masstransitHub");
app.MapHub<MassTransitChatHub>("/masstransitChatHub");

app.Run();