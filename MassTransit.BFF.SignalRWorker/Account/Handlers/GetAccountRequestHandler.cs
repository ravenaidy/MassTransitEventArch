using MassTransit;
using MediatR;
using SignalRWorker.Account.Queries;

namespace SignalRWorker.Account.Handlers
{
    public class GetAccountRequestHandler : IRequestHandler<GetAccountRequest>
    {
        private readonly ITopicProducer<GetAccountRequest> _producer;

        public GetAccountRequestHandler(ITopicProducer<GetAccountRequest> producer)
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
