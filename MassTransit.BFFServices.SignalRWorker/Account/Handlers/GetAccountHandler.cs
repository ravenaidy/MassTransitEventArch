using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Queries;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Handlers
{
    public class GetAccountHandler : IRequestHandler<GetAccountRequest>
    {
        private readonly ITopicProducer<GetAccountRequest> _producer;

        public GetAccountHandler(ITopicProducer<GetAccountRequest> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        
        public async Task<Unit> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            await _producer.Produce(request, cancellationToken);
            return Unit.Value;
        }
    }
}