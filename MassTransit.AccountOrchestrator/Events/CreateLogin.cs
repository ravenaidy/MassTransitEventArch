namespace MassTransit.AccountOrchestrator.Events
{
    public class CreateLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}