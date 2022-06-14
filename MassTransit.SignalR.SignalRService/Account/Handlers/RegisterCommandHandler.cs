using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Account.Commands;
using MediatR;

namespace MassTransit.SignalR.SignalRService.Account.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterAccountCommand>
    {
        private readonly ITopicProducer<RegisterAccountCommand> _producer;

        public RegisterCommandHandler(ITopicProducer<RegisterAccountCommand> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }
        public async Task<Unit> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            await _producer.Produce(request, cancellationToken);
            return Unit.Value;
        }
    }
}