using DrinkInfoPractice.Models;
using Spectre.Console;

namespace DrinkInfoPractice.Services;

internal class TableVisualization
{
    internal static void ShowTable(DrinkDetail drinkDetail)
    {
        List<string> ingredientList = new List<string>();
        foreach (var ingredient in drinkDetail.Ingredients)
        {
            ingredientList.Add($"- {ingredient.Value.Ingredients}: {ingredient.Value.Measure}");
        }

        string? ingredients = string.Join("\n", ingredientList);

        Console.WriteLine("\n");

        Table table = new Table();

        table.AddColumn("Drink ID");
        table.AddColumn("Drink");
        table.AddColumn("Category");
        table.AddColumn("Glass");
        table.AddColumn("Instructions");
        table.AddColumn("Ingredients");

        table.AddRow(
            drinkDetail.IdDrink,
            drinkDetail.StrDrink,
            drinkDetail.StrCategory,
            drinkDetail.StrGlass,
            drinkDetail.StrInstructions,
            ingredients
            );

        table.Border(TableBorder.Square);
        table.BorderColor(Color.Blue);

        AnsiConsole.Write(table);

        Console.WriteLine("Press Enter");
        Console.ReadLine();

    }
}
