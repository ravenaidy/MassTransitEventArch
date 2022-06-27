using MassTransit.SignalR.SignalRService.Models;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub
    {
        public async Task SendGetAccountRequest(GetAccountRequest request)
        {
            await Clients.All.SendAsync("RetrieveAccountRequest", request);
        }
        public async Task SendAccount(Account account)
        {
            await Clients.All.SendAsync("RetrieveAccount", account);
        }
    }
}