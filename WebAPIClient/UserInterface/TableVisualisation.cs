using Spectre.Console;
using System.Reflection;

namespace WebAPIClient.UserInterface
{
    public class TableVisualisation
    {
        public static async Task ShowTable<T>(List<T> tableData, string? tableName) where T : class
        {
            AnsiConsole.Clear();
            if (tableName == null)
                tableName = "";

            var table = new Table();
            string imageLink="";

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
                foreach (var value in values)
                {
                    if (value.Contains("jpg"))
                        imageLink = value;
                }

                table.AddRow(values);
            }
            if (!string.IsNullOrEmpty(imageLink))
            {
                await ImageVisualisation.GetImage(imageLink);
            }
            AnsiConsole.Write(table);
        }
    }
}
