using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Models;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub<IMassTransitAccountHub>
    {
        public async Task SendGetAccountRequest(GetAccountRequest request)
        {
            await Clients.All.PublishGetAccountRequest(request);
        }
        public async Task SendAccount(Account account)
        {
            await Clients.All.PublishAccount(account);
        }
    }
}