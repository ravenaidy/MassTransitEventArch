using MassTransit.SignalR.SignalRService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<MassTransitAccountHub>("/masstransitHub");

app.Run();