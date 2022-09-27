using System.Threading.Tasks;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Models;

namespace MassTransit.LoginService.Repositories.Contracts
{
    public interface ILoginRepository
    {
        Task RegisterLogin(CreateLogin login);
        Task<Login> GetLogin(GetLogin login);
    }
}