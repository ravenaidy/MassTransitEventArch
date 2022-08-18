using System;
using System.Security.Cryptography.X509Certificates;
using MassTransit.AccountOrchestrator.Configuration;
using MassTransit.AccountOrchestrator.Events.Login;

namespace MassTransit.AccountOrchestrator.StateMachine.Login
{
    public class LoginStateMachine : MassTransitStateMachine<LoginState>
    {
        public LoginStateMachine(LoginStateMachineSettings settings)
        {
            Event(() => LoginRequestEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => LoginResponseEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

            Request(() => AuthorizedUser, config => config.ServiceAddress = new Uri(settings.APIRequests.AuthTokenUrl));

            InstanceState(x => x.CurrentState, Login);

            Initially(
                When(LoginRequestEvent)
                    .Then(context => { context.Saga.Username = context.Message.Username; })
                    .Produce(context => context.Init<LoginRequest>(
                        new
                        {
                            context.Message.Username, context.Message.Password
                        }))
                    .TransitionTo(LoginReceived));

            During(LoginReceived, When(LoginResponseEvent)
                .If(x => x.Message.UserId <= 0, x => x.Finalize())
                .IfElse(x => x.Message.UserId > 0, x => x.Request(AuthorizedUser,
                        x => x.Init<GetAuthToken>(new GetAuthToken(x.Message.UserId, x.Saga.Username))),
                    x => x.TransitionTo(AuthorizedUser.Pending)));

            During(AuthorizedUser.Pending,
                When(AuthorizedUser.Completed)
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
