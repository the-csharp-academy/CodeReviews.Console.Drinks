using DrinkExplorer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DrinkExplorer.Database
{
    public class FavoriteDrinkRepository
    {
        private readonly string _connectionString;

        public FavoriteDrinkRepository() 
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found in appsettings.json.");
        }

        public void InsertFavouriteDrink(Drink drink)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("INSERT INTO FavouriteDrinks (DrinkId, DrinkName, Category, Alcoholic, Glass, Instructions, DrinkImageUrl) VALUES (@IdDrink, @StrDrink, @StrCategory, @StrAlcoholic, @StrGlass, @StrInstructions, @StrDrinkThumb)", connection);
            command.Parameters.AddWithValue("@IdDrink", drink.IdDrink);
            command.Parameters.AddWithValue("@StrDrink", drink.StrDrink);
            command.Parameters.AddWithValue("@StrCategory", drink.StrCategory);
            command.Parameters.AddWithValue("@StrAlcoholic", drink.StrAlcoholic);
            command.Parameters.AddWithValue("@StrGlass", drink.StrGlass);
            command.Parameters.AddWithValue("@StrInstructions", drink.StrInstructions);
            command.Parameters.AddWithValue("@StrDrinkThumb", drink.StrDrinkThumb);
            command.ExecuteNonQuery();
        }

        public DrinkResponse ShowAllFavourites()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM FavouriteDrinks ORDER BY DrinkName", connection);
            using var reader = command.ExecuteReader();
            var favouriteDrinks = new DrinkResponse
            {
                Drinks = new List<Drink>()
            };

            while (reader.Read())
            {
                favouriteDrinks.Drinks.Add(new Drink
                {
                    IdDrink = reader.GetString(1),
                    StrDrink = reader.GetString(2),
                    StrCategory = reader.IsDBNull(3) ? null : reader.GetString(3),
                    StrAlcoholic = reader.IsDBNull(4) ? null : reader.GetString(4),
                    StrGlass = reader.IsDBNull(5) ? null : reader.GetString(5),
                    StrInstructions = reader.IsDBNull(6) ? null : reader.GetString(6),
                    StrDrinkThumb = reader.IsDBNull(7) ? null : reader.GetString(7)
                });
            }
            return favouriteDrinks;
        }

        public void IncrementViewCount(Drink drink)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string query = """
            IF EXISTS
            (SELECT 1 FROM DrinkViews WHERE DrinkId = @DrinkId)
            BEGIN
            UPDATE DrinkViews SET ViewCount = ViewCount + 1 WHERE DrinkId = @DrinkId
            END
            ELSE
            BEGIN
            INSERT INTO DrinkViews (DrinkId, DrinkName, ViewCount ) VALUES( @DrinkId, @DrinkName, 1)
            END
            """;

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DrinkId",drink.IdDrink);
            command.Parameters.AddWithValue("@DrinkName", drink.StrDrink);
            command.ExecuteNonQuery();
        }

        public List<DrinkView> GetMostViewedDrinks()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM DrinkViews ORDER BY ViewCount DESC", connection);
            using var reader = command.ExecuteReader();
            var mostViewedDrinks = new List<DrinkView>();

            while (reader.Read())
            {
                mostViewedDrinks.Add(new DrinkView
                {
                    DrinkId = reader.GetString(0),
                    DrinkName = reader.GetString(1),   
                    ViewCount = reader.GetInt32(2)
                });
            }
            return mostViewedDrinks;
        }
    }
}
