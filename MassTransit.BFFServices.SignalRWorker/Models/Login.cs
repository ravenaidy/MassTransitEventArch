namespace MassTransit.BFFServices.SignalRWorker.Models
{
    public class Login  
    {
        public int LoginId { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}