using DrinksInfo.DB;
using DrinksInfo.Enums;
using DrinksInfo.Model;
using DrinksInfo.Repository;
using DrinksInfo.Service;
using Spectre.Console;

namespace DrinksInfo.Menu;
internal class FavoriteDrinkMenu
{
    private readonly FavoriteDrinkRepository favoriteDrinkRepository;
    private readonly DrinkViewRepository drinkViewRepository;
    private readonly CocktailService cocktailService = new();
    private readonly CocktailMenu userInput;

    public FavoriteDrinkMenu(DataBase dataBase)
    {
        favoriteDrinkRepository = new FavoriteDrinkRepository(dataBase);
        drinkViewRepository = new DrinkViewRepository(dataBase);
        userInput = new CocktailMenu(dataBase);
    }

    internal async Task Menu()
    {
        bool exit = true;

        while (exit)
        {
            Console.Clear();

            var options = AnsiConsole.Prompt(
                    new SelectionPrompt<FavoriteMenuOptions>()
                    .Title("Select the option")
                    .AddChoices(Enum.GetValues<FavoriteMenuOptions>()));

            switch (options)
            {
                case FavoriteMenuOptions.View:
                    await ViewMenu();
                    break;
                case FavoriteMenuOptions.Add:
                    await userInput.GetCategories();
                    break;
                case FavoriteMenuOptions.Remove:
                    RemoveMenu();
                    break;
                case FavoriteMenuOptions.Exit:
                    exit = false;
                    break;
            }
        }
    }

    private async Task ViewMenu()
    {
        List<FavoriteDrink> drinks = favoriteDrinkRepository.GetAll().ToList();

        if (drinks.Count == 0)
        {
            AnsiConsole.MarkupLine("There are no favorite drinks");
        }
        else
        {
            TableGenerator.ShowTable(drinks, "Favorite Drinks");

            bool showDetails = AnsiConsole.Confirm("Do you want to see the details of the drink?");

            if (showDetails)
            {
                await ShowDrinkDetails();
            }
        }

        AnsiConsole.MarkupLine("Press any key to continue");
        Console.ReadKey();
        return;
    }

    private void RemoveMenu()
    {
        List<FavoriteDrink> drinks = favoriteDrinkRepository.GetAll().ToList();

        TableGenerator.ShowTable(drinks, "Favorite Drinks");

        bool wasDeleted = favoriteDrinkRepository.Delete(GetIdDrink("Please write the id of the drink to remove"));

        if (wasDeleted)
        {
            AnsiConsole.MarkupLine("[green]Drink delete successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("Drink not found");
        }

        Console.ReadKey();

        return;
    }

    private async Task ShowDrinkDetails()
    {
        DrinkIDetail drink = await cocktailService.GetDrinkInfo(GetIdDrink("Please write the id of the drink to view"));

        await TableGenerator.ShowDetails(drink, "Drink Info");

        drinkViewRepository.AddView(Int32.Parse(drink.idDrink),drink.strDrink);

        Console.ReadKey();
    }

    private static string GetIdDrink(string message)
    {
        AnsiConsole.MarkupLine(message);

        string idDrink = Console.ReadLine();

        while (!ValidateInput.ValidateId(idDrink))
        {
            AnsiConsole.MarkupLine("[red]Invalid id[/]");
            idDrink = Console.ReadLine();
        }

        return idDrink;
    }
}
