using DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class DrinkListSelectionView
{

    public DrinkSummaryResponse Render(string categoryName, List<DrinkSummaryResponse> drinks)
    {
        Console.Clear();
        return AnsiConsole.Prompt(
                    new SelectionPrompt<DrinkSummaryResponse>()
                    .Title($"Select a drink from the {categoryName} list:")
                    .PageSize(15)
                    .EnableSearch()
                    .SearchPlaceholderText("Begin typing to search drink list...")
                    .UseConverter(d => $"{d.Name}")
                    .AddChoices(drinks));
    }
}
