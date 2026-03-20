using Dapper;
using DrinksInfo.Infrastructure.Interfaces;

namespace DrinksInfo.Infrastructure.Sqlite;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ISqliteConnectionFactory _connectionFactory;
    private readonly string FavoriteDrinkTableName = "FavoriteDrink";
    private readonly string DrinkViewCountTableName = "DrinkViewCount";


    public DatabaseInitializer(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Run()
    {
        InitializeFavoriteDrinkTable();
        InitializeDrinkViewCountTable();
    }

    private void InitializeFavoriteDrinkTable()
    {
        if (TableExists(FavoriteDrinkTableName) == false)
            CreateFavoriteDrinkTable();
    }

    private void CreateFavoriteDrinkTable()
    {
        string parameters = $@" Id integer primary key not null,
                                DrinkId integer not null,
                                Name text not null,
                                Category text not null";
        using var connection = _connectionFactory.CreateConnection();
        var command = connection.CreateCommand();
        command.CommandText = $"create table if not exists {FavoriteDrinkTableName}({parameters})";
        command.ExecuteNonQuery();
    }
    private void InitializeDrinkViewCountTable()
    {
        if (TableExists(DrinkViewCountTableName) == false)
            CreateDrinkViewCountTable();
    }

    private void CreateDrinkViewCountTable()
    {
        string parameters = $@" Id integer primary key not null,
                                DrinkId integer not null,
                                ViewCount integer not null";
        using var connection = _connectionFactory.CreateConnection();
        var command = connection.CreateCommand();
        command.CommandText = $"create table if not exists {DrinkViewCountTableName}({parameters})";
        command.ExecuteNonQuery();
    }

    private bool TableExists(string tableName)
    {
        var sql = $@"select count(*) from sqlite_master
                    where type='table' and name='{tableName}'";
        using var connection = _connectionFactory.CreateConnection();
        int count = connection.ExecuteScalar<int>(sql);

        return count == 1;
    }
}
