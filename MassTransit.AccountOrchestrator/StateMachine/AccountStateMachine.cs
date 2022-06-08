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
            Event(() => CreateAccountEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => AccountCreatedEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

            InstanceState(x => x.CurrentState, RegisterAccount);

            Initially(
                When(RegisterAccountEvent)
                    .Then(context =>
                    {
                        context.Saga.AddressLine1 = context.Message.AddressLine1;
                        context.Saga.AddressLine2 = context.Message.AddressLine2;
                        context.Saga.AddressLine3 = context.Message.AddressLine3;
                        context.Saga.City = context.Message.City;
                        context.Saga.PostalCode = context.Message.PostalCode;
                        context.Saga.Country = context.Message.Country;
                        context.Saga.Firstname = context.Message.Firstname;
                        context.Saga.Lastname = context.Message.Lastname;
                        context.Saga.Gender = context.Message.Gender;
                    })
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
                    .TransitionTo(CreateAccount));

            During(CreateAccount,
                When(LoginCreatedEvent)
                    .Produce(context => context.Init<CreateAccount>(new
                    {
                        AccountId = context.Saga.AccountId,
                        FirstName = context.Saga.Firstname,
                        LastName = context.Saga.Lastname,
                        Gender = (int)context.Saga.Gender,
                        AddressLine1 = context.Saga.AddressLine1,
                        AddressLine2 = context.Saga.AddressLine2,
                        AddressLine3 = context.Saga.AddressLine3,
                        City = context.Saga.City,
                        Country = context.Saga.Country,
                        PostalCode = context.Saga.PostalCode
                    }))
                    .TransitionTo(AccountCreated)
            );

            During(AccountCreated,
                When(AccountCreatedEvent)
                    .Finalize()
            );
        }
        
        public State RegisterAccount { get; set; }
        public State CreateLogin { get; set; }
        public State CreateAccount { get; set; }
        public State AccountCreated { get; set; }

        public Event<RegisterAccount> RegisterAccountEvent { get; private set; }
        public Event<CreateLogin> CreateLoginEvent { get; private set; }
        public Event<LoginCreated> LoginCreatedEvent { get; private set; }
        public Event<CreateAccount> CreateAccountEvent { get; private set; }
        public Event<AccountCreated> AccountCreatedEvent { get; private set; }
    }
}