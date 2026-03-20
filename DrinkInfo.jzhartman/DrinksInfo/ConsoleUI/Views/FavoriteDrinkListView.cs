using DrinksInfo.Application.Favorites.GetAllFavoriteDrinks;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class FavoriteDrinkListView
{
    public FavoriteDrinkResponse Render(List<FavoriteDrinkResponse> favoriteDrinks)
    {
        Console.Clear();
        return AnsiConsole.Prompt(
                    new SelectionPrompt<FavoriteDrinkResponse>()
                    .Title($"Select from your favorite drinks:")
                    .PageSize(15)
                    .EnableSearch()
                    .SearchPlaceholderText("Begin typing to search drink list...")
                    .UseConverter(f => $"{f.Name} ({f.Category})")
                    .AddChoices(favoriteDrinks));
    }
}
