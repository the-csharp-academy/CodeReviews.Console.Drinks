using DrinksInfo.DB;
using DrinksInfo.Enums;
using DrinksInfo.Model;
using DrinksInfo.Repository;
using DrinksInfo.Service;
using Spectre.Console;

namespace DrinksInfo.Menu;
internal class DrinkViewMenu
{
    private readonly DrinkViewRepository drinkViewRepository;
    private readonly CocktailService cocktailService;

    public DrinkViewMenu(DataBase dataBase)
    {
        drinkViewRepository = new DrinkViewRepository(dataBase);
    }

    public async Task Menu()
    {
        bool open = true;

        while (open)
        {
            Console.Clear();

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<DrinkViewMenuOptions>()
                .Title("Select the option")
                .AddChoices(Enum.GetValues<DrinkViewMenuOptions>()));

            switch (menuOption)
            {
                case DrinkViewMenuOptions.ShowDrinks:
                    Showdrinks();
                    break;
                case DrinkViewMenuOptions.AddView:
                    await AddView();
                    break;
                case DrinkViewMenuOptions.Exit:
                    open = false;
                    break;
            }
        }
    }

    public void Showdrinks()
    {
        List<DrinkView> drinks = drinkViewRepository.GetAll().ToList();

        if (drinks.Count == 0)
        {
            AnsiConsole.MarkupLine("There are no views of the drinks");
        }
        else
        {
            TableGenerator.ShowTable(drinks, "Drink Views");
        }

        Console.ReadKey();
    }

    public async Task AddView()
    {
        Showdrinks();

        AnsiConsole.MarkupLine("Please write the id of the drink to add a new view");

        string idDrink = Console.ReadLine();

        while (!ValidateInput.ValidateId(idDrink))
        {
            AnsiConsole.MarkupLine("[red]Invalid id[/]");
            idDrink = Console.ReadLine();
        }

        drinkViewRepository.AddView(Int32.Parse(idDrink), "");

        if (AnsiConsole.Confirm("You want to add new drink"))
        {
            await cocktailService.GetAllCategories();
        }      
    }
}