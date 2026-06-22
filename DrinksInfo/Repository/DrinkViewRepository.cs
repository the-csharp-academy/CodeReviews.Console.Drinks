using Dapper;
using DrinksInfo.DB;
using DrinksInfo.Model;
using Spectre.Console;

namespace DrinksInfo.Repository;
internal class DrinkViewRepository
{
    private readonly DataBase _dataBase;

    public DrinkViewRepository(DataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public IEnumerable<DrinkView> GetAll()
    {
        const string SQL = "SELECT IdDrink, DrinkName, DrinkViews FROM DrinkView";

        var connection = _dataBase.GetConnection();

        return connection.Query<DrinkView>(SQL);
    }

    public bool DrinkExists(int idDrink)
    {
        const string SQL = @"SELECT COUNT (*) FROM DrinkView WHERE IdDrink = @Id";

        using var connection = _dataBase.GetConnection();

        if ((long)connection.ExecuteScalar(SQL, new { Id = idDrink }) > 0)
        {
            return true;
        }

        return false;
    }

    public void AddView(int idDrink, string drinkName) 
    {
        using var connection = _dataBase.GetConnection();

        string sql;

        if (!DrinkExists(idDrink))
        {
            sql = @"INSERT INTO DrinkView (IdDrink, DrinkName, DrinkViews) VALUES (@Id, @Name,1)";

            connection.Execute(sql, new { Id = idDrink, Name = drinkName });
        }
        else
        {
            sql = @"UPDATE DrinkView SET DrinkViews = DrinkViews + 1 WHERE IdDrink = @Id";

            long rowsAffected = connection.Execute(sql, new { Id = idDrink });

            if (rowsAffected > 0)
            {
                AnsiConsole.MarkupLine("[green]View added successfully[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Drink not found[/]");
            }
        }
    }
}