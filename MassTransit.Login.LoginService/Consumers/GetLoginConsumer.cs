﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Extensions;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.LoginService.Consumers;

public class GetLoginConsumer : IConsumer<GetLogin>
{
    private readonly ILogger<GetLoginConsumer> _logger;
    private readonly ILoginRepository _loginRepository;
    private readonly IMapper _mapper;
    private readonly ITopicProducer<LoginResponse> _producer;

    public GetLoginConsumer(ILogger<GetLoginConsumer> logger, ILoginRepository loginRepository, IMapper mapper, ITopicProducer<LoginResponse> producer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async Task Consume(ConsumeContext<GetLogin> context)
    {
        _logger.LogDbRequest(nameof(LoginService), nameof(GetLoginConsumer), nameof(Consume), context.Message);
        
        try
        {
            var login = _mapper.Map<LoginResponse>(await _loginRepository.GetLogin(context.Message)) ??
                        new LoginResponse {LoginId = Guid.Empty};
            login.Enrich(x => x.CorrelationId = context.Message.CorrelationId);

            _logger.LogDbResponse(nameof(LoginService), nameof(GetLoginConsumer), nameof(Consume), login);

            await _producer.Produce(login);
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(LoginService),nameof(GetLoginConsumer),  nameof(Consume),
                context.Message.CorrelationId.ToString(), ex);
            throw;
        }
    }
}