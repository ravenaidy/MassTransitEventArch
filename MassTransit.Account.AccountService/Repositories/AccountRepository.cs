using System;
using System.Threading.Tasks;
using MassTransit.Account.AccountService.Events;
using MassTransit.Account.AccountService.Repositories.Constants;
using MassTransit.Account.AccountService.Repositories.Contracts;
using MassTransit.Shared.Infrastructure.Dapper;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;
using MassTransit.Shared.Infrastructure.Logger;
using Microsoft.Extensions.Logging;

namespace MassTransit.Account.AccountService.Repositories
{
    public class AccountRepository : DapperBaseRepository, IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(IConnectionFactory connectionFactory, ILogger<AccountRepository> logger) : base(connectionFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RegisterAccount(CreateAccount account)
        {
            var parameters = new
            {
                account.AccountId,
                account.Firstname,
                LastName = account.Lastname,
                account.Email,
                account.PhoneNumber,
                account.Gender,
                account.AddressLine1,
                account.AddressLine2,
                account.AddressLine3,
                account.City,
                account.Country,
                account.PostalCode
            };
            
            _logger.LogProcDetails(nameof(AccountService), nameof(AccountRepository), nameof(RegisterAccount),
                SPConstants.SPRegisterAccount, account.CorrelationId);
            await ExecuteAsync(SPConstants.SPRegisterAccount, parameters);
        }
    }
}