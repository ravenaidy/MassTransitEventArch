using System.Threading.Tasks;
using MassTransit.Account.AccountService.Repositories.Constants;
using MassTransit.Account.AccountService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Dapper;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

namespace MassTransit.Account.AccountService.Repositories
{
    public class AccountRepository : DapperBaseRepository, IAccountRepository
    {
        public AccountRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task RegisterAccount(Models.Account account)
        {
            await ExecuteAsync(SPConstants.SPRegisterAccount, account);
        }
    }
}