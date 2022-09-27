using System;
using System.Threading.Tasks;
using MassTransit.LoginService.Events;
using MassTransit.LoginService.Models;
using MassTransit.LoginService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Dapper;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.LoginService.Repositories
{
    public class LoginRepository : DapperBaseRepository, ILoginRepository
    {
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository(IConnectionFactory connectionFactory, ILogger<LoginRepository> logger) : base(connectionFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RegisterLogin(CreateLogin login)
        {
            var parameters = new {login.LoginId, UserName = login.Username, login.Password};
            _logger.LogProcDetails(nameof(LoginService), nameof(LoginRepository), nameof(RegisterLogin),
                SPConstants.SPRegisterLogin, login.CorrelationId);
            await ExecuteAsync(SPConstants.SPRegisterLogin, parameters);
        }

        public async Task<Login> GetLogin(GetLogin login)
        {
            var parameters = new {UserName = login.Username, Password = login.Password};
            _logger.LogProcDetails(nameof(LoginService), nameof(LoginRepository), nameof(GetLogin),
                SPConstants.SPGetLoginByUsernameAndPassword, login.CorrelationId);
            return await QueryFirstOrDefaultAsync<Login>(SPConstants.SPGetLoginByUsernameAndPassword, parameters);
        }
    }
}