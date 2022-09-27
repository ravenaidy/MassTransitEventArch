using System.Threading.Tasks;
using MassTransit.Account.AccountService.Events;

namespace MassTransit.Account.AccountService.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task RegisterAccount(CreateAccount account);
    }
}