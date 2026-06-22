namespace DrinksInfo.Menu;

using DrinksInfo.DB;
using DrinksInfo.Enums;
using DrinksInfo.Model;
using DrinksInfo.Repository;
using DrinksInfo.Service;
using Spectre.Console;
using System.Threading.Tasks;

internal class CocktailMenu
{
    private readonly CocktailService _cocktailService = new();
    private readonly FavoriteDrinkRepository favoriteDrinkService;
    private readonly DrinkViewRepository drinkViewRepository;

    internal CocktailMenu(DataBase dataBase)
    {
        favoriteDrinkService = new FavoriteDrinkRepository(dataBase);
        drinkViewRepository = new DrinkViewRepository(dataBase);
    }

    internal async Task GetCategories()
    {
        Console.Clear();

        List<Category> categories = await _cocktailService.GetAllCategories();

        TableGenerator.ShowTable(categories, "Drink Categories");

        AnsiConsole.MarkupLine("Write the category to show all the drinks");

        string category = Console.ReadLine();

        while (!ValidateInput.ValidateString(category))
        {
            AnsiConsole.MarkupLine("[red]Invalid category[/]");
            category = Console.ReadLine();
        }

        if (!categories.Any(x => x.strCategory == category))
        {
            AnsiConsole.MarkupLine("[yellow]Categorie not found[/]");
            await GetCategories();
        }

        await GetDrinks(category);
    }

    private async Task GetDrinks(string category)
    {
        List<Drink> drinks = await _cocktailService.GetAllDrinks(category);

        TableGenerator.ShowTable(drinks, "Drinks");

        AnsiConsole.MarkupLine("Write the id drink to show the info or type 0 to go back to all categories");

        string idDrink = Console.ReadLine();

        if (idDrink == "0") await GetCategories();

        while (!ValidateInput.ValidateId(idDrink))
        {
            AnsiConsole.MarkupLine("[red]Invalid id[/]");
            idDrink = Console.ReadLine();
        }

        if (!drinks.Any(x => x.idDrink == idDrink))
        {
            AnsiConsole.MarkupLine("[yellow]Categorie not found[/]");
            await GetCategories();
        }

        await GetDrinkInfo(idDrink, category);
    }

    private async Task GetDrinkInfo(string idDrink,string category)
    {
        DrinkIDetail drink = await _cocktailService.GetDrinkInfo(idDrink);

        await TableGenerator.ShowDetails(drink, "Drink Info");

        drinkViewRepository.AddView(Int32.Parse(drink.idDrink),drink.strDrink);

        bool addToFavorites = AnsiConsole.Confirm("Do you want to add the drink to favorites?");

        if (addToFavorites)
        {
            favoriteDrinkService.Insert(new FavoriteDrink
            {
                IdDrink = Int32.Parse(idDrink),
                Name = drink.strDrink
            });      
        }

        var options = AnsiConsole.Prompt(
            new SelectionPrompt<CocktailMenuOptions>()
            .Title("Select an option")
            .AddChoices(Enum.GetValues<CocktailMenuOptions>()));

        switch (options) 
        {
            case CocktailMenuOptions.GoCategories:
                await GetCategories();
                break;
            case CocktailMenuOptions.GoDrinks:
                Console.Clear();
                await GetDrinks(category);
                break;
            case CocktailMenuOptions.Exit:
                return;
        }
    }
}