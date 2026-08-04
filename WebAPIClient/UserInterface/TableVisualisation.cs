using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIClient.UserInterface
{
    internal class TableVisualisation
    {
        public static void ShowTable<T>(List<T> tableData, string? tableName) where T : class
        {
            Console.Clear();
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
