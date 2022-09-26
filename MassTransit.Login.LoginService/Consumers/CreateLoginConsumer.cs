using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.LoginService.Consumers;

public class CreateLoginConsumer : IConsumer<CreateLogin>
{
    private readonly ILogger<CreateLoginConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly ILoginRepository _loginRepository;
    private readonly ITopicProducer<LoginCreated> _producer;

    public CreateLoginConsumer(ILogger<CreateLoginConsumer> logger, IMapper mapper, ILoginRepository loginRepository,
        ITopicProducer<LoginCreated> producer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async Task Consume(ConsumeContext<CreateLogin> context)
    {
        _logger.LogDbRequest(nameof(LoginService), nameof(CreateLoginConsumer), nameof(Consume), context.Message);
        try
        {
            await _loginRepository.RegisterLogin(_mapper.Map<Login>(context.Message));
            var loginCreated = new LoginCreated
            {
                LoginId = context.Message.LoginId, LoginCreatedTimeStamp = InVar.Timestamp,
                CorrelationId = context.Message.CorrelationId
            };
            _logger.LogDbResponse(nameof(LoginService), nameof(CreateLoginConsumer), nameof(Consume), context.Message);
            
            await _producer.Produce(loginCreated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(LoginService), nameof(CreateLoginConsumer), nameof(Consume),
                context.Message.CorrelationId);
            throw;
        }
    }
}