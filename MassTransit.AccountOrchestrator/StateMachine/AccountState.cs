using System;
using MassTransit;

namespace MassTransit.AccountOrchestrator.StateMachine
{
    public class AccountState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
    }
}