using System.Data;
using DrinksInfo.davetn657.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DrinksInfo.davetn657.Controllers;

internal class FavouriteDrinkController
{
    private readonly string? _connectionString;

    public FavouriteDrinkController()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _connectionString = configuration.GetConnectionString("Default");

        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS Favourites (
                                    FavouriteId INT IDENTITY(1,1) PRIMARY KEY,
                                    DrinkId INT NOT NULL,
                                    FavouriteDrink NVARCHAR(50) NOT NULL);";

            tableCmd.ExecuteNonQuery();
        }
    }

    internal void AddFavourite(DrinkDto drink)
    {
        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"INSERT INTO Favourites (DrinkId, FavouriteDrink)
                                    VALUES (@Id, @Drink)";

            tableCmd.Parameters.Add("@Id", SqliteType.Integer).Value = drink.Id;
            tableCmd.Parameters.Add("@Drink", SqliteType.Text).Value = drink.Name;

            tableCmd.ExecuteNonQuery();
        }
    }

    internal void RemoveFavourite(DrinkDto drink)
    {
        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM Favourites
                                    WHERE DrinkId = @Id";

            tableCmd.Parameters.Add("@Id", SqliteType.Integer).Value = drink.Id;
            tableCmd.ExecuteNonQuery();
        }
    }

    internal List<DrinkDto> GetAllFavourites()
    {
        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT * FROM Favourites";

            var reader = tableCmd.ExecuteReader();
            var allFavourites = new List<DrinkDto>();

            while (reader.Read())
            {
                var drink = new DrinkDto();
                drink.Id = reader.GetInt32("DrinkId");
                drink.Name = reader.GetString("FavouriteDrink");

                allFavourites.Add(drink);
            }

            return allFavourites;
        }
    }

    internal bool CheckFavourite(DrinkDto drink)
    {
        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT * FROM Favourites
                                    WHERE DrinkId = @Id";

            tableCmd.Parameters.Add("@Id", SqliteType.Integer).Value = drink.Id;

            var reader = tableCmd.ExecuteReader();
            var areRows = false;

            if(!reader.Read()) areRows = true;

            return areRows;
        }
    }
}