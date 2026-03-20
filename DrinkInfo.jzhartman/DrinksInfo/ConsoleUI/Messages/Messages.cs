using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.Domain.Validation;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Output;

public class Messages
{
    public void RenderDrinkDetailKeyOptions(DrinkDetailEntryMode entryMode, bool isFavorite)
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Spectre.Console.Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Operation");

        table.AddRow("V", "View Drink Image");
        if (isFavorite == false)
            table.AddRow("F", "Add Drink to Favorites");
        if (isFavorite)
            table.AddRow("X", "Delete Drink from Favorites");
        if (entryMode == DrinkDetailEntryMode.Category)
        {
            table.AddRow("D", "Return to Drink Selection");
            table.AddRow("C", "Return to Category Selection");
        }
        if (entryMode == DrinkDetailEntryMode.Favorite)
        {
            table.AddRow("L", "Return to Favorites List");
        }
        table.AddRow("M", "Return to Main Menu");

        AnsiConsole.Write(table);
    }
    public void InputErrorMessage()
    {
        AnsiConsole.MarkupLine("[red]ERROR:[/] Invalid key press! Please enter one of the displayed options.");
    }

    public void OutputErrorMessage(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {error.Description}");
        }
        PressAnyKeyToContinue();
    }
    public void PrintAddFavoriteSuccessMessage(string name)
    {
        AnsiConsole.MarkupLine($"[green]SUCCESS:[/] Added {name} to favorites list!");
        PressAnyKeyToContinue();
    }
    public void PrintDeleteFavoriteSuccessMessage(string name)
    {
        AnsiConsole.MarkupLine($"[green]SUCCESS:[/] Deleted {name} from favorites list!");
        PressAnyKeyToContinue();
    }
    public void ExitMessage()
    {
        AnsiConsole.Status()
            .Spinner(new CheersSpinnerHelper())
            .Start("Cheers!", ctx =>
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            });
    }
    public void PressAnyKeyToContinue()
    {
        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }
}
