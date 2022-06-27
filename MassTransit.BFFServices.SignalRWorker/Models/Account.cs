using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;

namespace MassTransit.BFFServices.SignalRWorker.Models
{
    public class Account : IMapTo<MassTransit.SignalR.SignalRService.Models.Account>
    {
        
    }
}