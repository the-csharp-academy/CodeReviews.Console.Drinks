using System.Reflection;
using Spectre.Console;

namespace Drinks;

internal class UserInterface
{
    private DrinksService _drinksService;

    public UserInterface(DrinksService drinksService)
    {
        _drinksService = drinksService;
    }

    internal async Task CallMenu()
    {

        while (true)
        {
            try { await MenuFlow(); }
            catch (Refit.ApiRequestException ex)
            {
                AnsiConsole.MarkupLine("[red]Host is unreachable. Check your internet connection or the domain: http://www.thecocktaildb.com[/]");
            }

            if (AnsiConsole.Confirm("Quit the application?"))
                break;
        }
    }

    private async Task MenuFlow()
    {
        while (true)
        {
            Displayer.PrintTitleAndInstructions();

            // break if the API gives no responses, continue if the user cancel the operation
            Categories categories = await _drinksService.GetCategories();
            if (categories.ViewList == null)
                break;

            Category category = ChoicesPrompt.SelectOne(categories.ViewList, "[blue]Main Menu[/]");
            if (category.strCategory == "")
                break;

            Drinks drinks = await _drinksService.GetDrinksPerCategories(category.strCategory);
            if (drinks.ViewList == null)
                break;

            Drink drink = ChoicesPrompt.SelectOne(drinks.ViewList, $"[blue]Drink - {category.strCategory}[/]");
            if (drink.idDrink == "")
                continue;

            DrinkDetails drinkDetails = await _drinksService.GetDrinkDetails(drink.idDrink);

            Displayer.PrintTable(PrepareDrinkDetails(drinkDetails));

            AnsiConsole.MarkupLine("[grey] Press any key to go back to the main menu.[/]");
            Console.ReadKey();
        }
    }

    //-------------------------------------------------------------
    //---------------------- HELPERS ------------------------------
    //-------------------------------------------------------------

    private List<DisplayableDrinkDetails> PrepareDrinkDetails(DrinkDetails drinkDetails)
    {
        string formattedName = "";
        string content = "";
        const string instruction = "strInstructions";

        List<DisplayableDrinkDetails> detailsList = new();

        foreach (PropertyInfo property in typeof(DrinkDetails).GetProperties())
        {
            if (property.PropertyType != typeof(string))
                continue;

            // skip instructions translations
            if (property.Name.StartsWith("strInstructions") && property.Name.Length > instruction.Length)
                continue;

            // trim the first 3 letters of most properties
            if (property.Name.StartsWith("str"))
                formattedName = property.Name.Substring(3);
            else
                formattedName = property.Name;

            content = property.GetValue(drinkDetails) as string;

            if (!string.IsNullOrEmpty(content))
                detailsList.Add(new(formattedName, content));
        }
        return detailsList;
    }
}