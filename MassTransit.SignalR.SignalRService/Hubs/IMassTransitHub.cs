using System.Threading.Tasks;
using MassTransit.SignalR.SignalRService.DTO;
using MassTransit.SignalR.SignalRService.Models;

namespace MassTransit.SignalR.SignalRService.Hubs
{
    public interface IMassTransitAccountHub
    {
        Task PublishGetAccountRequest(GetAccountRequest request);
        Task PublishNewAccountRequest(NewAccountRequest request);
        Task PublishAccount(Account account);
    }
}