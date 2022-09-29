using MassTransit;
using Masstransit.Api.Auth.Events;
using Masstransit.Api.Auth.Services.Contracts;
using MassTransit.Shared.Infrastructure.Logger;

namespace Masstransit.Api.Auth.Consumers
{
  public class GetAuthTokenConsumer : IConsumer<GetAuthToken>
  {
    private readonly ILogger<GetAuthTokenConsumer> _logger;
    private readonly ITokenService _tokenService;
    private readonly ITopicProducer<AuthTokenResponse> _producer;

    public GetAuthTokenConsumer(ILogger<GetAuthTokenConsumer> logger, ITokenService tokenService, ITopicProducer<AuthTokenResponse> producer)
    {
      _logger = logger;
      _tokenService = tokenService;
      _producer = producer;
    }

    public async Task Consume(ConsumeContext<GetAuthToken> context)
    {
      _logger.LogRequestToken(nameof(Auth), nameof(GetAuthTokenConsumer), nameof(Consume), context.Message);

      var tokenResponse = new AuthTokenResponse
      {
        CorrelationId = context.Message.CorrelationId,
        Token = await _tokenService.GenerateToken(context.Message)
      };

      _logger.LogTokenResponse(nameof(Auth), nameof(GetAuthTokenConsumer), nameof(Consume), tokenResponse);

      await _producer.Produce(tokenResponse);
    }
  }
}
