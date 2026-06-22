using Spectre.Console;
using System.Reflection;

namespace DrinksInfo.Menu;
internal class TableGenerator
{
    internal static void ShowTable<T>(List<T> data, string title)
    {
        var table = new Table();

        table.Border(TableBorder.Rounded);
        table.Title($"[#A1E8CC]{title}[/]");

        PropertyInfo[] allProperties = typeof(T).GetProperties();

        var validProperties = allProperties
            .Where(property =>
                data.Any(item =>
                {
                    var value = property.GetValue(item);

                    return value != null && !string.IsNullOrWhiteSpace(value.ToString());
                }))
            .ToList();

        foreach (var property in validProperties)
        {
            string columnName = CleanPropertyName(property.Name);

            table.AddColumn($"[#454372]{columnName}[/]");
        }

        foreach (var item in data)
        {
            var values = validProperties
                .Select(p => p.GetValue(item)?.ToString() ?? "")
                .ToArray();

            table.AddRow(values);
        }

        AnsiConsole.Write(table);
    }

    internal static async Task ShowDetails<T>(T item, string title)
    {
        var table = new Table();

        table.Border(TableBorder.Rounded);
        table.Title($"[#A1E8CC]{title}[/]");

        table.AddColumn("[#454372]Detail[/]");
        table.AddColumn("[#454372]Value[/]");

        PropertyInfo[] properties = typeof(T).GetProperties().Where(p => p.GetIndexParameters().Length == 0).ToArray();

        foreach (var property in properties)
        {
            var rawValue = property.GetValue(item);

            if (rawValue == null) continue;

            string value = rawValue.ToString();

            if (string.IsNullOrWhiteSpace(value)) continue;

            string propertyName = CleanPropertyName(property.Name);

            table.AddRow(

                new Markup(Markup.Escape(propertyName)),
                new Markup(Markup.Escape(value))
            );
        }
        AnsiConsole.Write(table);
    }

    private static string CleanPropertyName(string name)
    {
        string prefix = "str";

        if (name.StartsWith(prefix))
        {
            name = name.Substring(prefix.Length);
        }

        return name;
    }

}