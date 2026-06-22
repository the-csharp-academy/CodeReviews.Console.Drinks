using Microsoft.Data.Sqlite;

namespace DrinksInfo.DB;
internal class DataBase
{
    private readonly string _connectionString;

    internal DataBase(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal SqliteConnection GetConnection() => new(_connectionString);

    internal void CreateTable() 
    {
        using var connection = GetConnection();

        connection.Open();

        var tablecmd = connection.CreateCommand();
        tablecmd.CommandText = @" CREATE TABLE IF NOT EXISTS FavoriteDrink(
            IdDrink INTEGER PRIMARY KEY,
            Name TEXT NOT NULL UNIQUE
            )";

        tablecmd.ExecuteNonQuery();

        tablecmd.CommandText = @"CREATE TABLE IF NOT EXISTS DrinkView(
            IdDrink INTEGER PRIMARY KEY,
            DrinkName TEXT NOT NULL UNIQUE,
            DrinkViews INTENGER DEFAULT 0)";

        tablecmd.ExecuteNonQuery();
    }
}