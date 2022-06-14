using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Account.Queries;
using MediatR;

namespace MassTransit.SignalR.SignalRService.Account.Handlers
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountRequest, Models.Account>
    {
        private readonly ITopicProducer<GetAccountRequest> _producer;

        public GetAccountQueryHandler(ITopicProducer<GetAccountRequest> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        
        public async Task<Models.Account> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            await _producer.Produce(request, cancellationToken);
        }
    }
}