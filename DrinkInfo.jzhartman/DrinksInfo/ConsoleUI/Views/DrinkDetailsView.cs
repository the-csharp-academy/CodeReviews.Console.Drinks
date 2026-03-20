using DrinksInfo.Application.DrinkInfoApi.GetDrinkDetailsById;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class DrinkDetailsView
{
    public void Render(DrinkDetailResponse drink, bool isFavorite, string viewCountText)
    {
        AnsiConsole.MarkupInterpolated($"[bold blue]Drink:[/] [bold underline green]{drink.Name}[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        var descriptionTable = GenerateDescriptionTable(drink, isFavorite, viewCountText);
        AnsiConsole.Write(descriptionTable);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold blue]Instructions:[/]");
        AnsiConsole.WriteLine($"{drink.Insructions}");

        var ingredientMeasurements = CombineIngredientsAndMeasurements(drink.Ingredients, drink.Measurements);


        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold blue]Ingredients:[/]");
        GenerateIngredientsList(ingredientMeasurements);
    }

    private Table GenerateDescriptionTable(DrinkDetailResponse drink, bool isFavorite, string viewCountText
        )
    {
        string spacing = "      ";
        var table = new Table()
                .HideHeaders()
                .NoBorder();

        table.AddColumn("Key");
        table.AddColumn("Value");
        table.AddColumn("Spacing");
        table.AddColumn("Key");
        table.AddColumn("Value");

        table.AddRow("[blue]Id:[/]", $"{drink.Id}", $"{spacing}", "[blue]Category:[/]", $"{drink.Category}");
        table.AddRow("[blue]Glass:[/]", $"{drink.Glass}", $"{spacing}", "[blue]Alcoholic:[/]", $"{drink.IsAlcoholic}");
        table.AddRow("[blue]Favorite:[/]", $"{isFavorite}", $"{spacing}", "[blue]View Count:[/]", $"{viewCountText}");

        return table;
    }

    private void GenerateIngredientsList(List<string> ingredients)
    {
        foreach (var item in ingredients)
        {
            AnsiConsole.WriteLine($"{item}");
        }
    }

    private List<string> CombineIngredientsAndMeasurements(List<string> ingredients, List<string> measurements)
    {
        var output = new List<string>();
        var ingredientCount = ingredients.Count();
        var measurementsCount = measurements.Count();

        var maxLength = (ingredientCount >= measurementsCount) ? ingredientCount : measurementsCount;

        for (int i = 0; i < maxLength; i++)
        {
            string ingredient = ingredients[i].Trim().Replace("\n", ""); ;
            string measurement = measurements[i].Trim().Replace("\n", ""); ;

            if (measurement != "<empty>" && ingredient != "<empty>")
                output.Add($"{measurement} {ingredient}");

            if (measurement == "<empty>" && ingredient != "<empty>")
                output.Add($"{ingredient}");

            if (measurement != "<empty>" && ingredient == "<empty>")
                output.Add($"{measurement} <UNKNOWN>");
        }

        return output;
    }
}
