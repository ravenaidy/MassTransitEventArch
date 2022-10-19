namespace MassTransit.SignalR.SignalRService.DTO
{
    public class ChatMessage
    {
        public int MessageId { get; set; }

        public string Message { get; set; }
        
        public string Username { get; set; }
    }
}