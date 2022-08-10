using System;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitChatHub : Hub
    {
        public async Task SendMessage(string group, string message)
        {
            var randomNumber = new Random().Next();
            await Clients.Group(group).SendAsync("PublishChatMessage",
                new ChatMessage {MessageId = randomNumber, Message = message});
        } 
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        public Task LeaveGroup(string group)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }
    }
}