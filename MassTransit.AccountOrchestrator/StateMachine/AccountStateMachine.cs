using MassTransit.AccountOrchestrator.Events;

namespace MassTransit.AccountOrchestrator.StateMachine
{
    public sealed class AccountStateMachine : MassTransitStateMachine<AccountState>
    {
        public AccountStateMachine()
        {
            Event(() => RegisterAccountEvent,
                x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => CreateLoginEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => LoginCreatedEvent,x  => x.CorrelateById(m => m.Message.CorrelationId));

            InstanceState(x => x.CurrentState, RegisterAccount);

            Initially(
                When(RegisterAccountEvent)
                    .Produce(context => context.Init<CreateLogin>(
                        new
                        {
                            Username = context.Message.Username,
                            Password = context.Message.Password
                        }))
                    .TransitionTo(CreateLogin));

            During(CreateLogin,
                When(LoginCreatedEvent)
                    .Then(context =>
                    {
                        context.Saga.AccountId = context.Message.UserId;
                        context.Saga.AccountCreated = context.Message.LoginCreatedTimeStamp;
                    })
                    .TransitionTo(LoginCreated),
                When(LoginCreatedEvent)
                    .TransitionTo(CreateAccount));
        }

        public State RegisterAccount { get; set; }
        public State CreateLogin { get; set; }
        public State LoginCreated { get; set; }
        public State CreateAccount { get; set; }
        //public State AccountCreated { get; set; }
        
        public Event<RegisterAccount> RegisterAccountEvent { get; private set; }
        public Event<CreateLogin> CreateLoginEvent { get; private set; }
         public Event<LoginCreated> LoginCreatedEvent { get; private set; }
        //public Event<CreateAccount> CreateAccountEvent { get; private set; }
        //public Event<AccountCreated> AccountCreatedEvent { get; private set; }
    }
}