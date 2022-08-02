namespace MassTransit.LoginService.Events
{
    public class GetLogin 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}