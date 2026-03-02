using DrinksInfo.davetn657.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DrinksInfo.davetn657.Controllers;

internal class ViewedDrinkController
{
    private readonly string? _connectionString;

    public ViewedDrinkController()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _connectionString = configuration.GetConnectionString("Default");

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS ViewedDrinks (
                                    DrinkId INT PRIMARY KEY,
                                    DrinkName TEXT NOT NULL,
                                    ViewCount INT NOT NULL DEFAULT 0);";

            tableCmd.ExecuteNonQuery();
        }
    }

    internal void AddDrink(DrinkDto drink)
    {
        using(var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"INSERT INTO ViewedDrinks (DrinkId, DrinkName)
                                    VALUES (@Id, @Drink)
                                    ON CONFLICT(DrinkId) DO UPDATE SET
                                        ViewCount = ViewCount + 1;";

            tableCmd.Parameters.Add("@Id", SqliteType.Integer).Value = drink.Id;
            tableCmd.Parameters.Add("@Drink", SqliteType.Text).Value = drink.Name;

            tableCmd.ExecuteNonQuery();
        }
    }

    internal int? GetViewCount(DrinkDto drink)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT ViewCount From ViewedDrinks
                                    WHERE DrinkId = @Id";

            tableCmd.Parameters.Add("@Id", SqliteType.Integer).Value = drink.Id;

            using var reader = tableCmd.ExecuteReader();

            if (reader.Read())
            {
                return reader.GetInt32("ViewCount");
            }

            return null;
        }
    }
}
