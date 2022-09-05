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

        public async Task RegisterLogin(Login login)
        {
            var parameters = new {login.LoginId, UserName = login.Username, login.Password };
            await ExecuteAsync(SPConstants.SPRegisterLogin, parameters);
        }

        public async Task<Login> GetLogin(string username, string password)
        {
            var parameters = new {UserName = username, Password = password};
            return await QueryFirstOrDefaultAsync<Login>(SPConstants.SPGetLoginByUsernameAndPassword, parameters);
        }
    }
}