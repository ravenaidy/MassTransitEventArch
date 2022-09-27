using System;
using MassTransit.Shared.Infrastructure.Events;
using Microsoft.Extensions.Logging;

namespace MassTransit.Shared.Infrastructure.Logger
{
  public partial class LoggerExtensions
  {
    private const string DbRequestInformationTemplate = "{Service} {Class} {Method} send database request: {@Message}";
    private const string DbResponseInformationTemplate = "{Service} {Class} {Method} successfully created db entry for {@Message}";
    private const string DbProcInformationTemplate = "{Service} {Class} {Method} calling {Proc}";
    private const string HubInformationTemplate = "{Service} {Hub} {Method} published message: {@Message} to subscriber";
    private const string PublishToHubTemplate =
      "{Service} {Class} {Method} successfully published registered account {@Message} to Hub";

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
    
    private static readonly Action<ILogger, string, string, string, string, Exception> _dbProcProcessor =
      LoggerMessage.Define<string, string, string, string>(
        LogLevel.Information,
        new EventId(100, ""),
        DbProcInformationTemplate);

    private static readonly Action<ILogger, string, string, string, IEvent, Exception> _hubRequestProcessor =
      LoggerMessage.Define<string, string, string, IEvent>(
        LogLevel.Information,
        new EventId(100, ""),
        HubInformationTemplate);
    
    private static readonly Action<ILogger, string, string, string, IEvent, Exception> _hubPublishProcessor =
      LoggerMessage.Define<string, string, string, IEvent>(
        LogLevel.Information,
        new EventId(100, ""),
        PublishToHubTemplate);

    public static void LogDbRequest(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _dbRequestProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId.ToString());
    }

    public static void LogDbResponse(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _dbResponseProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId.ToString());
    }
    
    public static void LogProcDetails(this ILogger logger, string serviceName, string className, string methodName,
      string procName, Guid correlationId)
    {
      Log(() => _dbProcProcessor(logger, serviceName, className, methodName, procName, null!),
        correlationId.ToString());
    }

    public static void LogHubInformation(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _hubRequestProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId.ToString());
    }
    
    public static void LogPublishToHubInformation(this ILogger logger, string serviceName, string className, string methodName,
      IEvent message)
    {
      Log(() => _hubPublishProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId.ToString());
    }
  }
}
