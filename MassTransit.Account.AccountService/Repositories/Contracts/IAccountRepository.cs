using System.Threading.Tasks;

namespace MassTransit.Account.AccountService.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task RegisterAccount(Models.Account account);
    }
}