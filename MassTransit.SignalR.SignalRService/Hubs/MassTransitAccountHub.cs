﻿using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using MassTransit.SignalR.SignalRService.Models;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub
    {
        public async Task SendLoginRequest(GetAccountRequest request)
        {
            await Clients.All.SendAsync("PublishGetAccountRequest", request);
        }

        public async Task SendLogin(Account account)
        {
            await Clients.All.SendAsync("PublishLogin", account);
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