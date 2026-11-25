using Spectre.Console;

namespace Drinks;

internal class Displayer
{
    internal static void PrintTable(List<DisplayableDrinkDetails> tableData)
    {
        // get the name string
        string drinksName = tableData[0].content;

        var table = new Table()
            .Title($"[blue]Drink Selected: {drinksName}[/]")
            .AddColumn("Name")
            .AddColumn("Content");

        foreach (var entry in tableData)
            table.AddRow(
                entry.contentTitle,
                entry.content);

        AnsiConsole.Write(table);
    }

    internal static void PrintTitleAndInstructions()
    {
        Console.Clear();
        Panel panel = new Panel("[blue]Drinks Menu[/]")
                    .BorderColor(Color.Blue);
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[grey]Press [Grey66]'escape'[/] to go back any time.[/]");
        AnsiConsole.MarkupLine("[grey]Pressing 'escape' on the main menu will quit.[/]");
        AnsiConsole.WriteLine();
    }

    internal static void CleanConsole()
    {

        Console.Clear();
    }
}