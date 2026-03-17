using System.Diagnostics.CodeAnalysis;
using Drinks_Info.Models;
using Spectre.Console;

namespace Drinks_Info
{
    internal class TableBuilder
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName)
            where T : class
        {
            bool isDrink = typeof(T) == typeof(Drink);
            Console.Clear();
            if (tableName == null)
            {
                tableName = "";
            }

            var table = new Table()
                .RoundedBorder();
            \
            if (isDrink)
            {
                table.AddColumn("ID");
                table.AddColumn(tableName);
            }
            else
            {
                table.AddColumn(tableName);
            }

            foreach (var item in tableData)
            {
                if (item is Drink drink)
                {
                    table.AddRow(drink.idDrink, drink.strDrink);
                }
                else
                {
                    table.AddRow(item.ToString()!);
                }
            }
            AnsiConsole.Write(table);

        }
        public static void ShowDetailsTable(List<object> tableData, string tableName)
        {
          

            Console.Clear();

            var table = new Table()
         
                .Border(TableBorder.Rounded)
                .ShowRowSeparators()
                .Title($"[bold yellow]{tableName}[/]")
                .AddColumn("[grey]Property[/]")
                .AddColumn("[white]Description[/]");

            foreach (dynamic item in tableData)
            {

                string key = item.Key?.ToString() ?? "";
                string value = item.Value?.ToString() ?? "";

                table.AddRow($"[blue]{key}[/]", value);
            }

            AnsiConsole.Write(table);
        }

    }
}

