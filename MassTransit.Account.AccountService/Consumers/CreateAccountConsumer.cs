using AutoMapper;
using MassTransit.Account.AccountService.Events;
using MassTransit.Account.AccountService.Repositories.Contracts;

namespace MassTransit.Account.AccountService.Consumers
{
    public class CreateAccountConsumer : IConsumer<CreateAccount>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ITopicProducer<AccountCreated> _producer;

        public CreateAccountConsumer(IAccountRepository accountRepository, IMapper mapper , ITopicProducer<AccountCreated> producer)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            var account = _mapper.Map<Models.Account>(context.Message);
            await _accountRepository.RegisterAccount(account);

            await _producer.Produce(new AccountCreated {CreatedTimeStamp = InVar.Timestamp});
        }
    }
}