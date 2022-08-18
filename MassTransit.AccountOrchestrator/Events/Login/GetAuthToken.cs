namespace MassTransit.AccountOrchestrator.Events.Login
{
    public record GetAuthToken(int UserId, string Username);
}
