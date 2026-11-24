using Spectre.Console;

namespace Drinks;

internal class Displayer
{
    internal static void PrintTable(List<DisplayableDrinkDetails> tableData)
    {
        var table = new Table()
            .Title("Details")
            .AddColumn("Name")
            .AddColumn("Content");

        foreach (var entry in tableData)
            table.AddRow(
                entry.contentTitle,
                entry.content);

        AnsiConsole.Write(table);
    }
}