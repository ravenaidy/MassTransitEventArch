using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Shared.Infrastructure.Events;
using MassTransit.Shared.Infrastructure.Logger;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker.Pipelines;

public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : IEvent
    where TRequest : IRequest<TResponse>, IEvent 
{
    private readonly ILogger<LoggingPipeline<TRequest, TResponse>> _logger;

    public LoggingPipeline(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogRequest(nameof(BFFServices), nameof(LoggingPipeline<TRequest, TResponse>), nameof(Handle), request);

        var response = await next();

        if (response is not Unit)
        {
            _logger.LogHandlerResponse(nameof(BFFServices), nameof(LoggingPipeline<TRequest, TResponse>), nameof(Handle), response);
        }

        return response;
    }
}