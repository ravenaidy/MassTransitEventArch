using System;
using Microsoft.Extensions.Logging;

namespace MassTransit.Shared.Infrastructure.Logger
{
  public partial class LoggerExtensions
  {
    private const string GenericExceptionTemplate = "{Service} {Class} {Method} with {CorrelationId} threw an exception: {@Exception}";

    private static readonly Action<ILogger, string, string, string, Guid, Exception> _genericExceptionProcessor =
      LoggerMessage.Define<string, string, string, Guid>(
        LogLevel.Error,
        new EventId(100, ""),
        GenericExceptionTemplate);

    public static void LogError(this ILogger logger, string serviceName, string className, string methodName,
      Guid correlationId, Exception ex)
    {
      Log(() => _genericExceptionProcessor(logger, serviceName, className, methodName, correlationId, ex),
        correlationId);
    }
  }
}
