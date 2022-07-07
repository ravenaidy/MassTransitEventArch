using System.Text.Json;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using MassTransit.SignalR.SignalRService.Models;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub
    {
        public async Task SendGetAccountRequest(GetAccountRequest request)
        {
            await Clients.All.SendAsync("PublishGetAccountRequest", request);
        }

        public async Task SendAccount(Account account)
        {
            await Clients.All.SendAsync("PublishAccount", account);
        }
        public async Task NewAccountRequest(string request)
        {
            await Clients.All.SendAsync("PublishNewAccountRequest", request);
        }

        public async Task AccountCreated(string request)
        {
            await Clients.All.SendAsync("PublishAccountCreated", request);
        }
    }
}