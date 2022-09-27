using System;
using Microsoft.Extensions.Logging;

namespace MassTransit.Shared.Infrastructure.Logger
{
  public partial class LoggerExtensions
  {
    private const string GenericExceptionTemplate = "{Service} {Class} {Method} with {CorrelationId} threw an exception";

    private static readonly Action<ILogger, string, string, string, string, Exception> _genericExceptionProcessor =
      LoggerMessage.Define<string, string, string, string>(
        LogLevel.Error,
        new EventId(100, ""),
        GenericExceptionTemplate);

    public static void LogError(this ILogger logger, string serviceName, string className, string methodName,
      string correlationId, Exception ex)
    {
      Log(() => _genericExceptionProcessor(logger, serviceName, className, methodName, correlationId, ex),
        correlationId);
    }
  }
}
