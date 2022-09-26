using System;
using MassTransit.Shared.Infrastructure.Events;
using Microsoft.Extensions.Logging;

namespace MassTransit.Shared.Infrastructure.Logger
{
  public partial class LoggerExtensions
  {
    private const string DbRequestInformationTemplate = "{Service} {Class} {Method} send database request: {@Message}";
    private const string DbResponseInformationTemplate = "{Service} {Class} {Method} successfully created db entry for {@Message}";
    private const string HubInformationTemplate = "{Service} {Hub} {Method} published message: {@Message} to subscriber";

    private static readonly Action<ILogger, string, string, string, IEvent, Exception> _dbRequestProcessor =
      LoggerMessage.Define<string, string, string, IEvent>(
        LogLevel.Information,
        new EventId(100, ""),
        DbRequestInformationTemplate);

    private static readonly Action<ILogger, string, string, string, IEvent, Exception> _dbResponseProcessor =
      LoggerMessage.Define<string, string, string, IEvent>(
        LogLevel.Information,
        new EventId(100, ""),
        DbResponseInformationTemplate);

    private static readonly Action<ILogger, string, string, string, IEvent, Exception> _hubRequestProcessor =
      LoggerMessage.Define<string, string, string, IEvent>(
        LogLevel.Information,
        new EventId(100, ""),
        HubInformationTemplate);

    public static void LogDbRequest(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _dbRequestProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
    }

    public static void LogDbResponse(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _dbResponseProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
    }

    public static void LogHubInformation(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _hubRequestProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
    }
  }
}
