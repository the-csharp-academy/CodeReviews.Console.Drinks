using Drinks.Models;
using Drinks.Services;
using Spectre.Console;
using System.Text.Json;

namespace Drinks
{
    internal static class UIController
    {
        private static readonly Category CancelCategory = new() { Name = "Cancel" };
        private static readonly Drink CancelDrink = new() { Name = "Cancel" };
        private static readonly Dictionary<string, string?> CancelFavourite = new() { ["strDrink"] = "Go Back" };

        private static readonly List<Dictionary<string, string?>> FavouriteDrinks = [];

        internal static async Task MainMenu()
        {
            bool exitApp = false;
            while (!exitApp)
            {
                AnsiConsole.Clear();
                string option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("The Cocktail Database Menu")
                    .HighlightStyle(Style.Parse("cyan"))
                    .AddChoices("View Drink Categories", "View Favourite Drinks", "Exit")
                    .WrapAround()
                );

                try
                {
                    switch (option)
                    {
                        case "View Drink Categories":
                            await ViewDrinkCategories();
                            break;
                        case "View Favourite Drinks":
                            await ViewFavouriteDrinks();
                            break;
                        case "Exit":
                            exitApp = true;
                            break;
                    }
                }
                catch (HttpRequestException ex)
                {
                    AnsiConsole.MarkupLine($"[red]Could not reach the API:[/] {Markup.Escape(ex.Message)}");
                    PromptGoBack();
                }
                catch (OperationCanceledException)
                {
                    AnsiConsole.MarkupLine("[red]The request timed out.[/]");
                    PromptGoBack();
                }
                catch (JsonException)
                {
                    AnsiConsole.MarkupLine("[red]The API sent data in an unexpected format.[/]");
                    PromptGoBack();
                }
            }
        }

        private static async Task ViewDrinkCategories()
        {
            List<Category> categories = await AnsiConsole.Status().StartAsync("Fetching the categories...", _ => DrinksService.GetCategories());
            bool goBack = false;
            while (!goBack)
            {
                AnsiConsole.Clear();
                Category? category = PromptSelection("Choose a Drink Category", categories, c => c.Name!, CancelCategory, "Go Back");
                if (category != null)
                {
                    await ViewDrinkList(category);
                }
                else
                {
                    goBack = true;
                }
            }
        }

        private static async Task ViewDrinkList(Category category)
        {
            List<Drink> drinks = await AnsiConsole.Status().StartAsync("Fetching the drinks...", _ => DrinksService.GetDrinksByCategory(category.Name!));
            bool goBack = false;
            while (!goBack)
            {
                AnsiConsole.Clear();
                Drink? drink = PromptSelection("Choose a Drink", drinks, d => d.Name!, CancelDrink, "Go Back");
                if (drink != null)
                {
                    Dictionary<string, string?>? drinkDetail = await AnsiConsole.Status().StartAsync("Fetching the drink details...", _ => DrinksService.GetDrinkById(drink.Id!));
                    if (drinkDetail != null)
                    {
                        await ShowDrinkDetail(drinkDetail);
                    }
                }
                else
                {
                    goBack = true;
                }
            }
        }

        private static async Task ViewFavouriteDrinks()
        {
            bool goBack = false;
            while (!goBack)
            {
                AnsiConsole.Clear();
                Dictionary<string, string?>? drinkDetail = PromptSelection("Favourite Drinks", FavouriteDrinks, f => Markup.Escape(f.GetValueOrDefault("strDrink") ?? "Unknown"), CancelFavourite, "Go Back");
                if (drinkDetail != null)
                {
                    await ShowDrinkDetail(drinkDetail);
                }
                else
                {
                    goBack = true;
                }
            }
        }

        private static T? PromptSelection<T>(string title, List<T> items, Func<T, string> label, T cancel, string exitMessage) where T : class
        {
            T selected = AnsiConsole.Prompt(
                new SelectionPrompt<T>()
                .Title(title)
                .HighlightStyle(Style.Parse("cyan"))
                .UseConverter(item => ReferenceEquals(item, cancel) ? $"[grey]{exitMessage}[/]" : Markup.Escape(label(item)))
                .AddChoices(items)
                .AddChoices(cancel)
                .PageSize(Math.Max(3, Console.WindowHeight - 4))
                .EnableSearch()
                .WrapAround());

            return ReferenceEquals(selected, cancel) ? null : selected;
        }

        private static async Task ShowDrinkDetail(Dictionary<string, string?> drinkDetail)
        {
            string? Get(string key) => drinkDetail.TryGetValue(key, out string? v) ? v : null;

            string? imageUrl = Get("strDrinkThumb");
            byte[]? bytes = null;
            if (imageUrl != null)
            {
                bytes = await AnsiConsole.Status().StartAsync("Loading image...", _ => DrinksService.GetImage(imageUrl + "/small"));
            }

            bool goBack = false;
            while (!goBack)
            {
                AnsiConsole.Clear();
                if (bytes != null)
                {
                    CanvasImage image = new CanvasImage(bytes);
                    image.MaxWidth(32);
                    AnsiConsole.Write(image);
                }

                var info = new Table().Border(TableBorder.Rounded).AddColumn("").AddColumn("");
                info.HideHeaders();
                AddIfPresent(info, "Name", Get("strDrink"));
                AddIfPresent(info, "Category", Get("strCategory"));
                AddIfPresent(info, "Type", Get("strAlcoholic"));
                AddIfPresent(info, "Glass", Get("strGlass"));
                AddIfPresent(info, "Instructions", Get("strInstructions"));
                AnsiConsole.Write(info);

                var ingredients = new Table().Border(TableBorder.Rounded).AddColumn("Ingredient").AddColumn("Measure");
                for (int i = 1; i <= 15; i++)
                {
                    string? ingredient = Get($"strIngredient{i}");
                    if (string.IsNullOrWhiteSpace(ingredient)) continue;

                    ingredients.AddRow(Markup.Escape(ingredient), Markup.Escape(Get($"strMeasure{i}") ?? ""));
                }
                AnsiConsole.Write(ingredients);

                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .HighlightStyle(Style.Parse("cyan"))
                    .AddChoices(FavouriteDrinks.Exists(d => SameDrink(d, drinkDetail)) ? "Remove from Favourites" : "Add to Favourites")
                    .AddChoices("Go Back")
                );

                if (choice == "Add to Favourites")
                {
                    string name = Get("strDrink")!;
                    FavouriteDrinks.Add(drinkDetail);
                }
                else if (choice == "Remove from Favourites")
                {
                    FavouriteDrinks.RemoveAll(d => SameDrink(d, drinkDetail));
                }
                else if(choice == "Go Back")
                {
                    goBack = true;
                }
            }
        }

        private static bool SameDrink(Dictionary<string, string?> a, Dictionary<string, string?> b) =>
        a.GetValueOrDefault("idDrink") == b.GetValueOrDefault("idDrink");

        private static void AddIfPresent(Table table, string label, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                table.AddRow(Markup.Escape(label), Markup.Escape(value));
            }
        }

        private static void PromptGoBack()
        {
            AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .HighlightStyle(Style.Parse("cyan"))
                .AddChoices("Go Back")
            );
        }
    }
}
