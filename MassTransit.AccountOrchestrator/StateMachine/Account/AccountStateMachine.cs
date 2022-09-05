using System;
using System.Threading.Tasks;
using MassTransit.AccountOrchestrator.Events.Accounts;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.AccountOrchestrator.StateMachine.Account;

public sealed class AccountStateMachine : MassTransitStateMachine<AccountState>
{
    public AccountStateMachine(ILogger<AccountStateMachine> logger)
    {
        if (logger is null) throw new ArgumentNullException(nameof(logger));

        Event(() => RegisterAccountEvent,
            x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => LoginCreatedEvent, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => AccountCreatedEvent, x => x.CorrelateById(m => m.Message.CorrelationId));

        InstanceState(x => x.CurrentState, RegisterAccount);

        Initially(
            When(RegisterAccountEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.AddressLine1 = context.Message.AddressLine1;
                    context.Saga.AddressLine2 = context.Message.AddressLine2;
                    context.Saga.AddressLine3 = context.Message.AddressLine3;
                    context.Saga.PhoneNumber = context.Message.PhoneNumber;
                    context.Saga.PostalCode = context.Message.PostalCode;
                    context.Saga.Country = context.Message.Country;
                    context.Saga.Firstname = context.Message.Firstname;
                    context.Saga.Lastname = context.Message.Lastname;
                    context.Saga.Gender = context.Message.Gender;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.City = context.Message.City;
                    
                    logger.LogEvent(nameof(AccountOrchestrator), nameof(AccountStateMachine),
                        nameof(RegisterAccountEvent), context.Message);
                })
                .Produce(context => context.Init<CreateLogin>(
                    new
                    {
                        LoginId = context.Message.UserId,
                        context.Message.Username,
                        context.Message.Password,
                        context.Message.CorrelationId
                    }))
                .TransitionTo(CreateLogin));

        During(CreateLogin,
            When(LoginCreatedEvent)
                .ThenAsync(context =>
                {
                    logger.LogEvent(nameof(AccountOrchestrator), nameof(AccountStateMachine),
                        nameof(LoginCreatedEvent), context.Message);
                    return Task.CompletedTask;
                })
                .Produce(context => context.Init<CreateAccount>(new
                {
                    AccountId = context.Saga.UserId,
                    FirstName = context.Saga.Firstname,
                    LastName = context.Saga.Lastname,
                    Gender = (int) context.Saga.Gender,
                    context.Saga.PhoneNumber,
                    context.Saga.AddressLine1,
                    context.Saga.AddressLine2,
                    context.Saga.AddressLine3,
                    context.Saga.City,
                    context.Saga.Country,
                    context.Saga.PostalCode,
                    context.Saga.Email,
                    context.Saga.CorrelationId
                }))
                .TransitionTo(CreateAccount));

        During(CreateAccount,
            When(AccountCreatedEvent)
                .ThenAsync(context =>
                {
                    logger.LogEvent(nameof(AccountOrchestrator), nameof(AccountStateMachine),
                        nameof(AccountCreatedEvent), context.Message);
                    return Task.CompletedTask;
                })
                .Produce(context => context.Init<AccountCreated>(new
                {
                    IsRegistered = true,
                    context.Message.CreatedTimeStamp,
                    LoginId = context.Saga.UserId,
                    context.Saga.CorrelationId
                }))
                .Finalize()
        );
        SetCompletedWhenFinalized();
    }

    public State RegisterAccount { get; set; }
    public State CreateLogin { get; set; }
    public State CreateAccount { get; set; }

    public Event<RegisterAccount> RegisterAccountEvent { get; private set; }
    public Event<LoginCreated> LoginCreatedEvent { get; private set; }
    public Event<AccountCreated> AccountCreatedEvent { get; private set; }
}