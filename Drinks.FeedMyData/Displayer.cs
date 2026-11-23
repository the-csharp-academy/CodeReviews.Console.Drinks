using Spectre.Console;

namespace Drinks;

public class Displayer
{
    public static void PrintTable<T>(List<T> tableData, Func<T, string?> getText, string tableName = "")
    {
        var table = new Table()
            .Title(tableName)
            .AddColumn("Info");

        foreach (var data in tableData)
            table.AddRow(getText(data) ?? "");

        AnsiConsole.Write(table);
    }

    public static void PrintTable<T>(
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