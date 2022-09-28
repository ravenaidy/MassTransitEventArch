using System;
using System.Threading.Tasks;
using MassTransit.AccountOrchestrator.Events.Login;
using MassTransit.AccountOrchestrator.StateMachine.Login;
using MassTransit.Orchestrator.Configuration;
using MassTransit.Orchestrator.Events.Login;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.Orchestrator.StateMachine.Login
{
    public class LoginStateMachine : MassTransitStateMachine<LoginState>
    {
        private readonly ILogger<LoginStateMachine> _logger;

        public LoginStateMachine(ILogger<LoginStateMachine> logger, LoginStateMachineSettings settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Event(() => LoginRequestEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => LoginResponseEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

            InstanceState(x => x.CurrentState, Login);

            Initially(
                When(LoginRequestEvent)
                    .ThenAsync(context =>
                    {
                        logger.LogEvent(nameof(AccountOrchestrator), nameof(LoginStateMachine),
                            nameof(LoginRequestEvent), context.Message);
                        context.Saga.Username = context.Message.Username;
                        return Task.CompletedTask;
                    })
                    .Produce(context => context.Init<LoginRequest>(
                        new
                        {
                            context.Message.Username, context.Message.Password, context.Message.CorrelationId
                        }))
                    .TransitionTo(LoginReceived)
                );

            During(LoginReceived, When(LoginResponseEvent)
                .If(x => x.Message.LoginId == Guid.Empty, act => act.ThenAsync(context =>
                    {
                        logger.LogNotFoundResponse(nameof(LoginReceived), context.Message);
                        return Task.CompletedTask;
                    }).Produce(context => context.Init<NoLogin>(new
                    {
                        context.Message.CorrelationId
                    })).Finalize()
                )
                .IfElse(x => x.Message.LoginId != Guid.Empty, act =>
                    act.Produce(context =>
                    {
                        var authToken = new GetAuthToken
                        {
                            Username = context.Message.Username,
                            CorrelationId = context.Message.CorrelationId,
                            UserId = context.Message.LoginId
                        };
                        logger.LogEvent(nameof(Orchestrator), nameof(LoginStateMachine), nameof(LoginReceived),
                            authToken);

                        return context.Init<GetAuthToken>(authToken);
                    }), x => x.TransitionTo(AuthTokenReceived)));
            
            SetCompletedWhenFinalized();
        }

        public State Login { get; set; }
        public State LoginReceived { get; set; }
        public State AuthTokenReceived { get; set; }
        public Event<LoginRequest> LoginRequestEvent { get; private set; }
        public Event<LoginResponse> LoginResponseEvent { get; private set; }
        public Event<GetAuthToken> AuthResponseEvent { get; private set; }
    }
}