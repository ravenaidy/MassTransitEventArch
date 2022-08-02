using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Queries;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Handlers
{
    public class GetLoginHandler : IRequestHandler<GetLoginRequest>
    {
        private readonly ITopicProducer<GetLoginRequest> _producer;

        public GetLoginHandler(ITopicProducer<GetLoginRequest> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        
        public async Task<Unit> Handle(GetLoginRequest handler, CancellationToken cancellationToken)
        {
            await _producer.Produce(handler, cancellationToken);
            return Unit.Value;
        }
    }
}