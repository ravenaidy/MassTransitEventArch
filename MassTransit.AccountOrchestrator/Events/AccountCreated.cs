namespace MassTransit.AccountOrchestrator.Events
{
    public record AccountCreated
    {
        public int AccountId { get; set; }
    }
}