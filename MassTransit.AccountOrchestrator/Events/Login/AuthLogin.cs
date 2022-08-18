namespace MassTransit.AccountOrchestrator.Events.Login
{
    public class AuthLogin
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    };

}
