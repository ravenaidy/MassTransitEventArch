using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub
    {
        public async Task SendLoginRequest(GetLoginRequest handler)
        {
            await Clients.All.SendAsync("PublishGetLoginRequest", handler);
        }

        public async Task SendLogin(LoginResponse login)
        {
            await Clients.All.SendAsync("PublishLogin", login);
        }
        public async Task SendNewAccountRequest(NewAccountRequest request)
        {
            await Clients.All.SendAsync("PublishNewAccountRequest", request);
        }

        public async Task SendAccountCreated(AccountRegistered request)
        {
            await Clients.All.SendAsync("PublishAccountCreated", request);
        }
    }
}