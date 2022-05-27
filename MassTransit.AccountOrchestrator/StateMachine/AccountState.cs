namespace MassTransit.AccountOrchestrator.StateMachine
{
    public class AccountState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int AccountId { get; set; }

        public DateTime LoginCreated { get; set; }
        public DateTime AccountCreated { get; set; }
        public int CurrentState { get; set; }
    }
}