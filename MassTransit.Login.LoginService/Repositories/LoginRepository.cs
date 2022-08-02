using System.Threading.Tasks;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Dapper;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

namespace MassTransit.LoginService.Repositories
{
    public class LoginRepository : DapperBaseRepository, ILoginRepository
    {
        public LoginRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> RegisterLogin(Login login)
        {
            var parameters = new {UserName = login.Username, login.Password };
            const string spName = "pr_RegisterLogin";
            return await QueryFirstOrDefaultAsync<int>(spName, parameters);
        }

        public async Task<Login> GetLogin(string username, string password)
        {
            var parameters = new {UserName = username, Password = password};
            const string spName = "pr_GetLoginByUsernameAndPassword";
            return await QueryFirstOrDefaultAsync<Login>(spName, parameters);
        }
    }
}