using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Login.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MassTransit.BFFServices.SignalRWorker.Login.Handlers
{
    public class GetLoginHandler : IRequestHandler<GetLoginRequest>
    {
        private readonly ILogger<GetLoginHandler> _logger;
        private readonly ITopicProducer<GetLoginRequest> _producer;

        public GetLoginHandler(ILogger<GetLoginHandler> logger, ITopicProducer<GetLoginRequest> producer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        
        public async Task<Unit> Handle(GetLoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing new login request for Id:{MessageCorrelationId}",
                request.CorrelationId.ToString());
            try
            {
                await _producer.Produce(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create login with Id {request.CorrelationId.ToString()} threw an exception: ", ex);
                throw;
            }

            _logger.LogInformation("Login request for Id:{MessageCorrelationId} was successfully published",
                request.CorrelationId.ToString());
            return Unit.Value;
        }
    }
}