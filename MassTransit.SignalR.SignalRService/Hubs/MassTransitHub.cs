using MassTransit.SignalR.SignalRService.Models;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitHub : Hub
    {
        public async Task SendRegister(Account account)
        {
            
        }
    }
}