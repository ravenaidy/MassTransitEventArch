using System.Threading.Tasks;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Dapper;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

namespace MassTransit.LoginService.Repositories
{
    public class LoginRepository : DapperBaseRepository, ILoginRepository
    {
        protected LoginRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> RegisterLogin(Login login)
        {
            var parameters = new {UserName = login.Username, login.Password };
            const string spName = "pr_CreateAccount";
            return await ExecuteAsync(spName, parameters);
        }
    }
}