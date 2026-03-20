using DrinksInfo.Application.Favorites.GetAllFavoriteDrinks;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class FavoriteDrinkServices
{
    private readonly GetAllFavoriteDrinksHandler _getAllFavoriteDrinksHandler;
    private readonly Messages _messages;
    private readonly FavoriteDrinkListView _favoriteDrinkList;
    private readonly DrinkDetailService _drinkDetailService;

    public FavoriteDrinkServices(GetAllFavoriteDrinksHandler getAllFavoriteDrinksHandler, Messages messages,
                                FavoriteDrinkListView favoriteDrinkList, DrinkDetailService drinkDetailService)
    {
        _getAllFavoriteDrinksHandler = getAllFavoriteDrinksHandler;
        _messages = messages;
        _favoriteDrinkList = favoriteDrinkList;
        _drinkDetailService = drinkDetailService;
    }

    public async Task RunAsync()
    {
        var exitCode = ExitCode.None;

        while (exitCode != ExitCode.MainMenu)
        {
            Console.Clear();
            var favoriteListResult = await ConsoleStatusHelper.ShowStatusAsync("Fetching favorite drink list...", () =>
                                        _getAllFavoriteDrinksHandler.HandleAsync());

            if (favoriteListResult.IsSuccess && favoriteListResult.Value is not null)
            {
                var drinkSelection = _favoriteDrinkList.Render(favoriteListResult.Value);

                exitCode = await _drinkDetailService.RunAsync(DrinkDetailEntryMode.Favorite,
                                                new(drinkSelection.DrinkId, drinkSelection.Name, "", drinkSelection.Category));
            }
            else
            {
                _messages.OutputErrorMessage(favoriteListResult.Errors);
                exitCode = ExitCode.MainMenu;
            }
        }
    }
}
