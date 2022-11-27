using System;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Hubs;
using MassTransit.SignalR.SignalRService.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .UseSerilog((ctx, log) =>
        log.ReadFrom.Configuration(ctx.Configuration));

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

builder.Services
    .AddAuthentication()
    .AddHubTokenAuthenticationScheme();

builder.Services.AddAuthorization(options =>
{
    
});

builder.Services.AddSignalR();
var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<MassTransitAccountHub>("/masstransitHub");
app.MapHub<MassTransitChatHub>("/masstransitChatHub");

app.Run();