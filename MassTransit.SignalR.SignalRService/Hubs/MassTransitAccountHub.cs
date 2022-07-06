using System.Text.Json;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
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
        public async Task NewAccountRequest(string request)
        {
            var decodedRequest = JsonSerializer.Deserialize<NewAccountRequest>(request, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (decodedRequest != null)
            {
                await Clients.All.PublishNewAccountRequest(decodedRequest);
            }
        }
    }
}