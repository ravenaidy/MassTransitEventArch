using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using MassTransit.Shared.Infrastructure.Logger;

namespace MassTransit.BFFServices.SignalRWorker.Account.Handlers;

public class NewAccountRequestHandler : IRequestHandler<NewAccountRequest>
{
    private readonly ILogger<NewAccountRequestHandler> _logger;
    private readonly ITopicProducer<NewAccountRequest> _producer;

    public NewAccountRequestHandler(ILogger<NewAccountRequestHandler> logger,
        ITopicProducer<NewAccountRequest> producer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async Task<Unit> Handle(NewAccountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _producer.Produce(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(BFFServices), nameof(NewAccountRequestHandler), nameof(Handle),
                request.CorrelationId.ToString(), ex);
            throw;
        }

        return Unit.Value;
    }
}