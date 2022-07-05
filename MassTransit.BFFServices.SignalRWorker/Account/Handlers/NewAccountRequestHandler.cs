using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.BFFServices.SignalRWorker.Account.Commands;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Handlers
{
    public class NewAccountRequestHandler : IRequestHandler<NewAccountRequest>
    {
        private readonly ITopicProducer<NewAccountRequest> _producer;

        public NewAccountRequestHandler(ITopicProducer<NewAccountRequest> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task<Unit> Handle(NewAccountRequest request, CancellationToken cancellationToken)
        {
            await _producer.Produce(request, cancellationToken);
            return Unit.Value;
        }
    }
}