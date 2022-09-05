using System;
using MassTransit.Shared.Infrastructure.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;


namespace MassTransit.Shared.Infrastructure.Logger;

public static class LoggerExtentions 
{
    public static void LogHubInformation<T>(this ILogger<T> logger, string serviceName, string hubName, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Hub} {Method} published message: {@Message} to subscriber", serviceName,
                hubName, methodName, message);
        }
    }
    public static void LogDbRequest<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} send database request: {@Message}", serviceName,
                className, methodName, message);
        }
    }

    public static void LogRequest<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} send message request: {@Message}", serviceName,
                className, methodName, message);
        }
    }
    
    public static void LogEvent<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} publishing message: {@Message} to message bus" , serviceName,
                className, methodName, message);
        }
    }
    
    public static void LogHandlerRequest<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} handler request: {@Message}", serviceName,
                className, methodName, message);
        }
    }
    
    public static void LogHandlerResponse<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} handler response: {@Message}", serviceName,
                className, methodName, message);
        }
    }
    
    public static void LogDbSuccess<T>(this ILogger<T> logger, string serviceName, string className, string methodName, IEvent message)
    {
        using (LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            logger.LogInformation("{Service} {Class} {Method} successfully created db entry for {@Message}", serviceName,
                className, methodName, message);
        }
    }
    
    public static void LogError<T>(this ILogger<T> logger, Exception ex, string serviceName, string className, string methodName, Guid correlationId)
    {
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            logger.LogError(ex, "{Service} {Class} {Method} with {CorrelationId} threw an exception:", serviceName,
                className, methodName, correlationId);
        }
    }
}