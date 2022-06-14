using System;
using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public class MassTransitHub : Hub
    {
        private readonly IMediator _mediator;

        public MassTransitHub(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        public async Task SendRegister(RegisterAccountCommand account)
        {
            await _mediator.Send(account);
        }
    }
}