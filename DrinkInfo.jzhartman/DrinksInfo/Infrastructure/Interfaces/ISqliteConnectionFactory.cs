using System.Data;

namespace DrinksInfo.Infrastructure.Interfaces;

public interface ISqliteConnectionFactory
{
    IDbConnection CreateConnection();
}