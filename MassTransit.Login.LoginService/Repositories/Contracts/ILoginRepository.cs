using System.Threading.Tasks;
using MassTransit.LoginService.Models;

namespace MassTransit.LoginService.Repositories.Contracts
{
    public interface ILoginRepository
    {
        Task RegisterLogin(Login login);
        Task<Login> GetLogin(string username, string password);
    }
}