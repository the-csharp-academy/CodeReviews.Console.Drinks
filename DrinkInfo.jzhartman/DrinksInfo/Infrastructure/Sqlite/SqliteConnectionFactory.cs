using DrinksInfo.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DrinksInfo.Infrastructure.Sqlite;

public class SqliteConnectionFactory : ISqliteConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
