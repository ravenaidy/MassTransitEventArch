using System;
using System.Security.Cryptography.X509Certificates;
using MassTransit.AccountOrchestrator.Configuration;
using MassTransit.AccountOrchestrator.Events.Login;
using Microsoft.Extensions.Logging;

namespace MassTransit.AccountOrchestrator.StateMachine.Login
{
    public class LoginStateMachine : MassTransitStateMachine<LoginState>
    {
        private readonly ILogger<LoginStateMachine> _logger;

        public LoginStateMachine(ILogger<LoginStateMachine> logger, LoginStateMachineSettings settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Event(() => LoginRequestEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => LoginResponseEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

            Request(() => AuthorizedUser, config => config.ServiceAddress = new Uri(settings.APIRequests.AuthTokenUrl));

            InstanceState(x => x.CurrentState, Login);

            Initially(
                When(LoginRequestEvent)
                    .Then(context =>
                    {
                        logger.LogInformation("Publish Login request");
                        context.Saga.Username = context.Message.Username;
                    })
                    .Produce(context => context.Init<LoginRequest>(
                        new
                        {
                            context.Message.Username, context.Message.Password, context.Message.CorrelationId
                        }))
                    .TransitionTo(LoginReceived));

            During(LoginReceived, When(LoginResponseEvent)
                .If(x => x.Message.LoginId == Guid.Empty, x => x.Finalize())
                .IfElse(x => x.Message.LoginId != Guid.Empty, x =>
                    {
                        _logger.LogInformation("Requesting auth token");
                        return x.Request(AuthorizedUser,
                            x => x.Init<GetAuthToken>(new GetAuthToken
                            {
                                UserId = x.Message.LoginId, Username = x.Saga.Username,
                                CorrelationId = x.Message.CorrelationId
                            }));
                    },
                    x => x.TransitionTo(AuthorizedUser.Pending)));

            During(AuthorizedUser.Pending,
                When(AuthorizedUser.Completed).Then(x => { _logger.LogInformation("Request was successful"); })
                    .Produce(context => context.Init<AuthLogin>(new
                    {
                        context.Saga.UserId,
                        context.Saga.Username,
                        context.Message.Token
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        public State Login { get; set; }
        public State LoginReceived { get; set; }

        public Request<LoginState, GetAuthToken, AuthLogin> AuthorizedUser { get; private set; }
        public Event<LoginRequest> LoginRequestEvent { get; private set; }
        public Event<LoginResponse> LoginResponseEvent { get; private set; }
    }
}