using System;
using System.Threading.Tasks;
using MassTransit.Shared.Infrastructure.Logger;
using MassTransit.SignalR.SignalRService.DTO;
using MassTransit.SignalR.SignalRService.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitAccountHub : Hub
    {
        private readonly ILogger<MassTransitAccountHub> _logger;

        public MassTransitAccountHub(ILogger<MassTransitAccountHub> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendLoginRequest(GetLoginRequest request)
        {
            _logger.LogHubInformation(nameof(SignalRService), nameof(MassTransitAccountHub), nameof(SendLoginRequest),
                request);
            await Clients.All.SendAsync("PublishGetLoginRequest", request);
        }

        public async Task SendLogin(LoginResponse login)
        {
            _logger.LogHubInformation(nameof(SignalRService), nameof(MassTransitAccountHub), nameof(SendLogin),
                login);
            await Clients.All.SendAsync("PublishLogin", login);
        }
        
        public async Task NoLogin(NoLogin login)
        {
            _logger.LogHubInformation(nameof(SignalRService), nameof(MassTransitAccountHub), nameof(NoLogin),
                login);
            await Clients.All.SendAsync("NoLogin", login);
        }

        public async Task SendNewAccountRequest(NewAccountRequest request)
        {

            _logger.LogHubInformation(nameof(SignalRService), nameof(MassTransitAccountHub),
                nameof(SendNewAccountRequest),
                request);
            await Clients.All.SendAsync("PublishNewAccountRequest", request);
        }

        public async Task SendAccountCreated(AccountRegistered request)
        {
            _logger.LogHubInformation(nameof(SignalRService), nameof(MassTransitAccountHub), nameof(SendAccountCreated),
                request);
            await Clients.All.SendAsync("PublishAccountCreated", request);
        }
    }
}