using Spectre.Console;

namespace Drinks;

internal class Displayer
{
    internal static void PrintTable<T>(List<T> tableData, Func<T, string?> getText, string tableName = "")
    {
        var table = new Table()
            .Title(tableName)
            .AddColumn("Info");

        foreach (var data in tableData)
            table.AddRow(getText(data) ?? "");

        AnsiConsole.Write(table);
    }

    internal static void PrintTable<T>(
        List<T> tableData,
        Func<T, string?> getKey,
        Func<T, string?> getValue,
        string tableName = "")
    {
        var table = new Table()
            .Title(tableName)
            .AddColumn("Field")
            .AddColumn("Value");

        foreach (var data in tableData)
            table.AddRow(getKey(data) ?? "", getValue(data) ?? "");

        AnsiConsole.Write(table);
    }
}