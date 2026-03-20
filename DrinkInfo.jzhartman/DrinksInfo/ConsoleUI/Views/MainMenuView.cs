using DrinksInfo.ConsoleUI.Enums;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class MainMenuView
{
    public MainMenuItem Render(MainMenuItem[] menuItems)
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuItem>()
                    .Title($"Select a menu option:")
                    .UseConverter(m => m switch
                    {
                        MainMenuItem.Browse => "Browse Drink Menu",
                        MainMenuItem.Favorites => "Manage Favorite Drinks",
                        MainMenuItem.Exit => "Exit Application",
                        _ => m.ToString()
                    }
                    )
                    .AddChoices(menuItems));

        return selection;
    }
}
