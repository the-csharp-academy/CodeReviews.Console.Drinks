using DrinksInfo.DB;
using DrinksInfo.Enums;
using Spectre.Console;

namespace DrinksInfo.Menu;
internal class Menu
{
    private readonly CocktailMenu cocktailMenu;
    private readonly FavoriteDrinkMenu favoriteDrinkMenu;
    private readonly DrinkViewMenu drinkViewMenu;

    internal Menu(DataBase dataBase) 
    {
        cocktailMenu = new CocktailMenu(dataBase);
        favoriteDrinkMenu = new FavoriteDrinkMenu(dataBase);
        drinkViewMenu = new DrinkViewMenu(dataBase);    
    }

    internal async Task MainMenu()
    {
        bool open = true;

        while (open)
        {
            Console.Clear();

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Select the option")
                .AddChoices(Enum.GetValues<MainMenuOptions>()));

            switch (menuOption)
            {
                case MainMenuOptions.ShowCategories:
                    await cocktailMenu.GetCategories();
                    break;
                case MainMenuOptions.ShowFavoriteDrinks:
                    await favoriteDrinkMenu.Menu();
                    break;
                case MainMenuOptions.ShowDrinkViews:
                    await drinkViewMenu.Menu();
                    break;
                case MainMenuOptions.Exit:
                    open = false;
                    break;
            }
        }
    }
}