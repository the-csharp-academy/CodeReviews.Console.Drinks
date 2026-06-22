using Dapper;
using DrinksInfo.DB;
using DrinksInfo.Model;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace DrinksInfo.Repository;
internal class FavoriteDrinkRepository
{
    private readonly DataBase _dataBase;

    internal FavoriteDrinkRepository(DataBase dataBase)
    {
        _dataBase = dataBase;
    }

    internal void Insert(FavoriteDrink favoriteDrink)
    {
        try
        {

            const string SQL = @"INSERT INTO FavoriteDrink (Name,IdDrink) VALUES (@Name,@IdDrink)";

            using var connection = _dataBase.GetConnection();
            connection.Execute(SQL, favoriteDrink);

            AnsiConsole.MarkupLine("[green]Drink added succesfully[/]");
        }
        catch (SqliteException ex)
        {
            if (ex.SqliteErrorCode == 19)
            {
                Console.WriteLine("Ese registro ya existe.");
            }
            else
            {
                Console.WriteLine($"Error SQLite: {ex.Message}");
            }
        }
    }

    internal IEnumerable<FavoriteDrink> GetAll()
    {
        const string SQL = "SELECT IdDrink,Name FROM FavoriteDrink ORDER BY IdDrink ASC";

        using var connection = _dataBase.GetConnection();
        return connection.Query<FavoriteDrink>(SQL);
    }

    internal bool Delete(string id)         
    {
        const string SQL = "DELETE FROM FavoriteDrink WHERE IdDrink = @Id";

        using var connection = _dataBase.GetConnection();
        int rowsAffected = connection.Execute(SQL, new { Id = id });

        if (rowsAffected == 0) return false;

        return true;
    }
}