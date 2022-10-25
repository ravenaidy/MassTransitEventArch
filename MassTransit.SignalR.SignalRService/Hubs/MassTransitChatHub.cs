using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitChatHub : Hub
    {
        [Authorize]
        public async Task SendMessage(string group, ChatMessage message)
        {
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