using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class MainMenuService
{
    private readonly Messages _messages;
    private readonly MainMenuView _mainMenu;
    private readonly CategoryListService _categoryList;
    private readonly FavoriteDrinkServices _favoriteDrinkServices;
    public MainMenuService(Messages messages, MainMenuView mainMenu, CategoryListService categoryList,
                        FavoriteDrinkServices favoriteDrinkServices)
    {
        _messages = messages;
        _mainMenu = mainMenu;
        _categoryList = categoryList;
        _favoriteDrinkServices = favoriteDrinkServices;
    }
    public async Task RunAsync()
    {
        bool exitApp = false;
        MainMenuItem[] menuItems = Enum.GetValues<MainMenuItem>();

        while (exitApp == false)
        {
            Console.Clear();
            var selection = _mainMenu.Render(menuItems);

            switch (selection)
            {
                case MainMenuItem.Browse:
                    await _categoryList.RunAsync();
                    break;
                case MainMenuItem.Favorites:
                    await _favoriteDrinkServices.RunAsync();
                    break;
                case MainMenuItem.Exit:
                    exitApp = true;
                    break;
            }
        }
        _messages.ExitMessage();
    }
}
