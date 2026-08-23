namespace Drinks;

using System.Diagnostics.CodeAnalysis;
using ConsoleTableExt;
using Spectre.Console;
using System.Reflection;

public class TableVisualizationEngine
{
    public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T :
    class
    {

        Console.Clear();

        if (tableName == null)
        {
            tableName = "";
        }

        Console.WriteLine("\n\n");

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Title($"[bold yellow]{tableName}[/]");

        PropertyInfo[] allProperties = typeof(T).GetProperties();

        PropertyInfo[] propertiesithValue = allProperties.Where(p => tableData.Any(item => !string.IsNullOrWhiteSpace(p.GetValue(item)?.ToString()))).ToArray();

        foreach (PropertyInfo prop in propertiesithValue)
        {
            if (prop.Name.Contains("str"))
            {
                string formattedName = prop.Name.Substring(3);
                table.AddColumn($"[blue]{formattedName}[/]");
            }
            else
            {
                table.AddColumn($"[blue]{prop.Name}[/]");
            }
        }

        foreach (var item in tableData)
        {
            var rowValues = propertiesithValue.Select(p => p.GetValue(item)?.ToString() ?? "").ToArray();
            table.AddRow(rowValues);
        }

        AnsiConsole.Write(table);

        return ;

    }
}