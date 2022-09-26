using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.Account.AccountService.Events;
using MassTransit.Account.AccountService.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using MassTransit.Shared.Infrastructure.Logger;

namespace MassTransit.Account.AccountService.Consumers;

public class CreateAccountConsumer : IConsumer<CreateAccount>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAccountConsumer> _logger;
    private readonly IAccountRepository _accountRepository;
    private readonly ITopicProducer<AccountCreated> _producer;

    public CreateAccountConsumer(ILogger<CreateAccountConsumer> logger, IAccountRepository accountRepository, IMapper mapper,
        ITopicProducer<AccountCreated> producer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async Task Consume(ConsumeContext<CreateAccount> context)
    {
        _logger.LogDbRequest(nameof(AccountService), nameof(CreateAccountConsumer), nameof(Consume), context.Message);
        
        try
        {
            var account = _mapper.Map<Models.Account>(context.Message);
            await _accountRepository.RegisterAccount(account);

            var createdAccount = new AccountCreated
            {
                AccountId = context.Message.AccountId, CreatedTimeStamp = InVar.Timestamp,
                CorrelationId = context.Message.CorrelationId
            };
            _logger.LogDbResponse(nameof(AccountService), nameof(CreateAccountConsumer), nameof(Consume), createdAccount);
            
            await _producer.Produce(createdAccount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(AccountService),nameof(CreateAccountConsumer),  nameof(Consume),
                context.Message.CorrelationId);
            throw;
        }
    }
}