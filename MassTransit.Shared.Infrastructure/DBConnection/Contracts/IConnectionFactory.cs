using System.Data;

namespace MassTransit.Shared.Infrastructure.DBConnection.Contracts
{
    public interface IConnectionFactory
    {
        IDbConnection CreateOpenConnection();
    }
}