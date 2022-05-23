using MassTransit;
using MassTransit.AccountOrchestrator.Events;

namespace MassTransit.AccountOrchestrator.StateMachine
{
    public sealed class AccountStateMachine : MassTransitStateMachine<AccountState>
    {
        public AccountStateMachine()
        {
            Event(() => RegisterAccountEvent);
            Event(() => CreateLoginEvent);
            Event(() => LoginCreatedEvent);
            
            InstanceState(x => x.CurrentState, RegisterAccount);
            Initially(
                When(RegisterAccountEvent)
                    .TransitionTo(RegisterAccount),
                When(CreateLoginEvent)
                    .TransitionTo(CreateLogin),
                When(LoginCreatedEvent)
                    .TransitionTo(LoginCreated));
        }
        
        public State RegisterAccount { get; set; }
        public State CreateLogin { get; set; }
        public State LoginCreated { get; set; }
        //public State CreateAccount { get; set; }
        //public State AccountCreated { get; set; }
        
        public Event<RegisterAccount> RegisterAccountEvent { get; private set; }
        public Event<CreateLogin> CreateLoginEvent { get; private set; }
        public Event<LoginCreated> LoginCreatedEvent { get; private set; }
        //public Event<CreateAccount> CreateAccountEvent { get; private set; }
        //public Event<AccountCreated> AccountCreatedEvent { get; private set; }
    }
}