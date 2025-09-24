using Spectre.Console;

namespace DrinkInfoPractice.Services;

internal class UserInput
{
    CocktailService cocktailService = new CocktailService();

    internal async Task Start()
    {
        bool continueRun = true;
        while (continueRun)
        {
            Console.Clear();

            string category = await CategoryMenu();

            string id = await DrinksMenu(category);

            TableVisualization.ShowTable(await cocktailService.GetDrinkByIdAsync(id));

            continueRun = ContinueRun();
        }
    }

    internal bool ContinueRun()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Would you like to find another drink?")
            .AddChoices(new[]
            {
                "Yes",
                "No"
            }));
        return choice == "Yes";
    }

    private async Task<string> DrinksMenu(string category)
    {
        var drinks = await cocktailService.GetDrinksByCategoryAsync(category);

        Dictionary<string, string> drinkMapping = drinks.ToDictionary(d => d.StrDrink, d => d.IdDrink);

        var drinkNames = drinkMapping.Keys.ToList();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a drink")
            .AddChoices(drinkNames));

        string id = drinkMapping[choice].ToString();
        return id;
    }

    internal async Task<string> CategoryMenu()
    {
        var categories = await cocktailService.GetCategoriesAsync();

        var categoryNames = categories.Select(c => c.StrCategory).ToList();
        categoryNames.Add("Exit");

        var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Selection the category of drink.")
            .AddChoices(categoryNames));

        if (category == "Exit") Environment.Exit(0);

        return category;
    }
}
