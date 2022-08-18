using System;

namespace MassTransit.AccountOrchestrator.StateMachine.Login
{
    public class LoginState : SagaStateMachineInstance
    {
        public int UserId { get; set; }
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public string Username { get; set; }
    }
}
