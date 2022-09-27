using System;
using System.Data;
using MassTransit.Shared.Infrastructure.DBConnection.Contracts;

namespace MassTransit.Shared.Infrastructure.DBConnection
{
    public class ConnectionFactory<TConnection> : IConnectionFactory where TConnection : IDbConnection, new()
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentException(null, nameof(connectionString));
        }

        public IDbConnection CreateOpenConnection()
        {
            var connection = new TConnection {ConnectionString = _connectionString};

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Error when opening connection to Database");
            }
            return connection;
        }
    }
}