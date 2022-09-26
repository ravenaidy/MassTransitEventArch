using System;
using MassTransit.Shared.Infrastructure.Events;
using Microsoft.Extensions.Logging;

namespace MassTransit.Shared.Infrastructure.Logger;

public partial class LoggerExtensions
{
  private const string RequestInformationTemplate = "{Service} {Class} {Method} send message request: {@Message}";
  private const string EventInformationTemplate = "{Service} {Class} {Method} publishing message: {@Message} to message bus";
  private const string HandlerRequestInformationTemplate = "{Service} {Class} {Method} handler request: {@Message}";
  private const string HandlerResponseInformationTemplate = "{Service} {Class} {Method} handler response: {@Message}";

  private static readonly Action<ILogger, string, string, string, IEvent, Exception> _requestInfoProcessor =
    LoggerMessage.Define<string, string, string, IEvent>(
      LogLevel.Information,
      new EventId(100, ""),
      RequestInformationTemplate);

  private static readonly Action<ILogger, string, string, string, IEvent, Exception> _eventInfoProcessor =
    LoggerMessage.Define<string, string, string, IEvent>(
      LogLevel.Information,
      new EventId(100, ""),
      EventInformationTemplate);

  private static readonly Action<ILogger, string, string, string, IEvent, Exception> _handlerRequestInfoProcessor =
    LoggerMessage.Define<string, string, string, IEvent>(
      LogLevel.Information,
      new EventId(100, ""),
      HandlerRequestInformationTemplate);

  private static readonly Action<ILogger, string, string, string, IEvent, Exception> _handlerResponseInfoProcessor =
    LoggerMessage.Define<string, string, string, IEvent>(
      LogLevel.Information,
      new EventId(100, ""),
      HandlerResponseInformationTemplate);


  public static void LogRequest(this ILogger logger, string serviceName, string className, string methodName,
    IEvent message)
  {
    Log(() => _requestInfoProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
  }

  public static void LogEvent(this ILogger logger, string serviceName, string className, string methodName,
    IEvent message)
  {
    Log(() => _eventInfoProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
  }

  public static void LogHandlerRequest(this ILogger logger, string serviceName, string className, string methodName,
    IEvent message)
  {
    Log(() => _handlerRequestInfoProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
  }
  public static void LogHandlerResponse(this ILogger logger, string serviceName, string className, string methodName,
    IEvent message)
  {
    Log(() => _handlerResponseInfoProcessor(logger, serviceName, className, methodName, message, null!), message.CorrelationId);
  }
}

