using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Login.Events;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker.Login.Consumers;

public class NoLoginFoundConsumer : IConsumer<NoLogin>
{
  private readonly ILogger<NoLoginFoundConsumer> _logger;
  private readonly HubConnection _hubContext;

  public NoLoginFoundConsumer(ILogger<NoLoginFoundConsumer> logger, HubConnection hubContext)
  {
    _logger = logger;
    _hubContext = hubContext;
  }

  public async Task Consume(ConsumeContext<NoLogin> context)
  {
    _logger.LogPublishToHubInformation(nameof(BFFServices), nameof(NoLoginFoundConsumer), nameof(Consume),
      context.Message);
    await _hubContext.InvokeAsync("NoLogin", context.Message);
  }
}