using ConsoleTableExt;
using Spectre.Console;
using System.Reflection;

namespace WebAPIClient.UserInterface
{
    public class TableVisualisation
    {
        public static void ShowTable<T>(List<T> tableData, string? tableName) where T : class
        {
            Console.Clear();
            if (tableName == null)
                tableName = "";

            var table = new Table();
            

            if (!string.IsNullOrEmpty(tableName))
            {
                table.Title(tableName);
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                table.AddColumn(property.Name);
            }

            foreach (var item in tableData)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty).ToArray();
                table.AddRow(values);
            }

            AnsiConsole.Write(table);
        }
    }
}
