using CodeReviews.Console.Drinks.Models;
using CodeReviews.Console.Drinks.Services;
using Spectre.Console;
using Spectre.Console.Extensions;

try
{
    AnsiConsole.MarkupLine("[blue]Welcome to the Cocktail Database Explorer![/]\n");

    using var httpClient = new HttpClient();
    var drinkService = new DrinkService(httpClient);

    var categories = await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .StartAsync("Fetching drink categories...", async ctx =>
        {
            return await drinkService.GetCategoriesAsync();
        });

    if (!categories.Any())
    {
        AnsiConsole.MarkupLine("[red]No categories found. Please check your internet connection.[/]");
        return;
    }

    var selectedCategory = AnsiConsole.Prompt(
        new SelectionPrompt<DrinkCategory>()
            .Title("[green]Choose a drink category:[/]")
            .PageSize(10)
            .UseConverter(category => category.StrCategory)
            .AddChoices(categories)
    );

    var drinks = await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .StartAsync($"Fetching {selectedCategory.StrCategory} drinks...", async ctx =>
        {
            return await drinkService.GetDrinksByCategoryAsync(selectedCategory.StrCategory);
        });

    if (!drinks.Any())
    {
        AnsiConsole.MarkupLine($"[red]No drinks found in the {selectedCategory.StrCategory} category.[/]");
        return;
    }

    var selectedDrink = AnsiConsole.Prompt(
        new SelectionPrompt<Drink>()
            .Title($"[green]Choose a {selectedCategory.StrCategory} drink:[/]")
            .PageSize(10)
            .UseConverter(drink => drink.StrDrink)
            .AddChoices(drinks)
    );

    var drinkDetail = await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .StartAsync($"Fetching details for {selectedDrink.StrDrink}...", async ctx =>
        {
            return await drinkService.GetDrinkDetailAsync(selectedDrink.IdDrink);
        });

    if (drinkDetail == null)
    {
        AnsiConsole.MarkupLine("[red]Drink details not found.[/]");
        return;
    }

    DisplayDrinkInformation(selectedDrink, drinkDetail);
}
catch (HttpRequestException ex)
{
    AnsiConsole.MarkupLine($"[red]Network error: {ex.Message}[/]");
    AnsiConsole.MarkupLine("[dim]Please check your internet connection and try again.[/]");
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]An unexpected error occurred: {ex.Message}[/]");
}

static void DisplayDrinkInformation(Drink selectedDrink, DrinkDetail drinkDetail)
{
    var headerPanel = new Panel($"[bold yellow]{selectedDrink.StrDrink}[/]")
        .Header("[green]Drink Information[/]", Justify.Center)
        .Border(BoxBorder.Double)
        .Expand();

    AnsiConsole.Write(headerPanel);
    AnsiConsole.WriteLine();

    var infoTable = new Table()
        .Border(TableBorder.Rounded)
        .AddColumn("[blue]Property[/]")
        .AddColumn("[blue]Value[/]");

    infoTable.AddRow("Category", drinkDetail.StrCategory ?? "Not specified");
    infoTable.AddRow("Type", drinkDetail.StrAlcoholic ?? "Not specified");
    infoTable.AddRow("Glass", drinkDetail.StrGlass ?? "Not specified");
    infoTable.AddRow("IBA", drinkDetail.StrIBA ?? "Not specified");
    infoTable.AddRow("Tags", drinkDetail.StrTags ?? "None");

    AnsiConsole.Write(infoTable);
    AnsiConsole.WriteLine();

    if (!string.IsNullOrWhiteSpace(drinkDetail.StrInstructions))
    {
        var instructionsPanel = new Panel($"[italic]{drinkDetail.StrInstructions}[/]")
            .Header("[green]Instructions[/]", Justify.Left)
            .Expand();
        
        AnsiConsole.Write(instructionsPanel);
        AnsiConsole.WriteLine();
    }

    var ingredients = drinkDetail.Ingredients.ToList();
    
    if (ingredients.Any())
    {
        var ingredientsTable = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]Ingredient[/]")
            .AddColumn("[yellow]Measure[/]");

        foreach (var (ingredient, measure) in ingredients)
        {
            ingredientsTable.AddRow(ingredient, measure ?? "To taste");
        }

        var ingredientsPanel = new Panel(ingredientsTable)
            .Header("[green]Ingredients[/]", Justify.Left)
            .Expand();

        AnsiConsole.Write(ingredientsPanel);
        AnsiConsole.WriteLine();
    }

    if (!string.IsNullOrWhiteSpace(drinkDetail.StrImageSource))
    {
        AnsiConsole.MarkupLine(
            $"[dim]Image: {drinkDetail.StrImageSource} (by {drinkDetail.StrImageAttribution})[/]");
    }

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[dim]Press any key to exit...[/]");
    Console.ReadKey();
}
