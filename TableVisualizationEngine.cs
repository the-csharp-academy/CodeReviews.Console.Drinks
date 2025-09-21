using System.Diagnostics.CodeAnalysis;
using ConsoleTableExt;

namespace Drinks_App
{
    class TableVisualizationEngine
    {
        /*
         * Displays any list of objects as a console table.
         *
         * <T> is a generic type parameter:
         *     - When you call this method, T becomes the actual model type
         *       (e.g., User, Product, Order, etc.).
         *     - This allows a single method to handle tables for any model,
         *       instead of writing one method per model type.
         *
         * tableData : List<T>   → the list of objects to print
         * tableName : string?   → optional header name (can be null)
         */
        public static void ShowTable <T>(List<T> TableData, [AllowNull] string TableName) where T : class
        {
            Console.Clear();

            if (TableName == null) TableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(TableData).WithColumn(TableName).WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine(TableAligntment.Center);

            Console.WriteLine("\n\n");

        }
    }
}
