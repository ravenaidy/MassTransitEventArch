using System;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    [Authorize]
    public class MassTransitChatHub : Hub
    {
        public async Task SendMessage(string group, ChatMessage message)
        {
            var randomNumber = new Random().Next();
            await Clients.Group(group).SendAsync("PublishChatMessage", message);
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