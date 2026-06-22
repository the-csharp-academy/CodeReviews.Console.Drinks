using DrinksInfo.DB;
using DrinksInfo.Menu;
using Microsoft.Extensions.Configuration;

class Program
{
    public static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("settings.json",optional: false)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        DataBase dataBase = new DataBase(connectionString);
        dataBase.CreateTable();

        Menu menu = new Menu(dataBase);

        await menu.MainMenu();

    }
}