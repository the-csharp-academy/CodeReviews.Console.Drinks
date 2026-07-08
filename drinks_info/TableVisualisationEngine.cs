using System;
using Spectre.Console;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.IO;
using System.Net.Http;

namespace drinks_info
{
    public class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
        {
            Console.Clear();

            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

             var table = new Table().Title(tableName);

            PropertyInfo[] properties = typeof(T).GetProperties();
            if (typeof(T) == typeof(object) && tableData != null && tableData.Count > 0 && tableData[0] != null)
            {
                properties = tableData[0].GetType().GetProperties();
            }
            else
            {
                properties = typeof(T).GetProperties();
            }

            foreach (PropertyInfo prop in properties)
            {
                table.AddColumn(prop.Name);
            }

            if(tableData == null)
            {
                Console.WriteLine("No record found.");
                return;
            }

            foreach(T item in tableData)
            {
                List<String> rowValues = new();

                foreach(PropertyInfo prop in properties)
                {
                    object value = prop.GetValue(item);
                    rowValues.Add(value?.ToString() ?? "");
                }

                table.AddRow(rowValues.ToArray());
            }

            AnsiConsole.Write(table);
        }

        public static void ShowImage(MemoryStream ms)
        {
            var image = new CanvasImage(ms).MaxWidth(80);

            AnsiConsole.Write(image);
        }

        public static void ShowImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            try
            {
                using HttpClient client = new HttpClient();
                byte[] imageBytes = client.GetByteArrayAsync(imageUrl).GetAwaiter().GetResult();
                using MemoryStream ms = new MemoryStream(imageBytes);
                ShowImage(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not load image: {ex.Message}");
            }
        }
    }
}
