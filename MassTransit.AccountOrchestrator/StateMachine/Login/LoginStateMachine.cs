using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.AccountOrchestrator.Events.Login;
using MassTransit.Mediator;

namespace MassTransit.AccountOrchestrator.StateMachine.Login
{
    public class LoginStateMachine : MassTransitStateMachine<LoginState>
    {
        public LoginStateMachine()
        {
            Event(() => LoginRequestEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

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
                .Request(AuthorisedUser, x=> x.Init<GetAuthToken>(new GetAuthToken(x.Message.UserId, x.Saga.Username)), r => r.);
        }

        public State Login { get; set; }
        public State LoginReceived { get; set; }

        public Request<LoginState, GetAuthToken, AuthLogin> AuthorisedUser { get; private set; }
        public Event<LoginRequest> LoginRequestEvent { get; private set; }
        public Event<LoginResponse> LoginResponseEvent { get; private set; }
    }
}
