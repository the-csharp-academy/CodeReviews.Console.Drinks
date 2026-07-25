using DrinkExplorer.Database;
using DrinkExplorer.Models;
using DrinkExplorer.Validation;
using SixLabors.ImageSharp;
using Spectre.Console;
using System.Diagnostics;
using System.Net.Http.Json;

namespace DrinkExplorer.Services
{
    public class ManageDrinksService
    {
        private readonly HttpClient _httpClient;
        private readonly ConsoleUI _consoleUI;
        private readonly InputValidator _inputValidator;
        private readonly FavoriteDrinkRepository _favoriteDrinkRepository;

        public ManageDrinksService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/")
            };
            _consoleUI = new ConsoleUI();
            _inputValidator = new InputValidator();
            _favoriteDrinkRepository = new FavoriteDrinkRepository();
        }

        public async Task ViewAllCategoriesAsync()
        {
            var categories = await GetCategoriesAsync();
            if (categories.Count == 0)
            {
                _consoleUI.ShowError("No categories found.");
                _consoleUI.Pause();
                return;
            }

            var selectedCategory = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Select a category[/]")
                    .AddChoices(categories));

            var drinks = await GetDrinksByCategoryAsync(selectedCategory);
            if (drinks.Count == 0)
            {
                _consoleUI.ShowError($"No drinks found in {selectedCategory}.");
                _consoleUI.Pause();
                return;
            }

            var selectedDrink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                    .Title($"[bold cyan]Select a drink from {selectedCategory}[/]")
                    .UseConverter(drink =>drink.StrDrink ?? "Unknown Drink")
                    .AddChoices(drinks));

            var drinkDetails = await GetDrinkByIdAsync(selectedDrink.IdDrink!);

            if (drinkDetails == null)
            {
                _consoleUI.ShowError("Unable to retrieve drink details.");
                _consoleUI.Pause();
                return;
            }
            _favoriteDrinkRepository.IncrementViewCount(drinkDetails);
            await DisplayDrinkDetailsAsync(drinkDetails);
        }

        public async Task SearchDrinksAsync()
        {
            try
            {
                var drinkName = AnsiConsole.Ask<string>("[bold cyan]Enter the drink name to search:[/]");
                if (!_inputValidator.ValidateDrinkName(drinkName))
                {
                    _consoleUI.ShowError("Error fetching the Drink");
                    _consoleUI.Pause();
                    return;
                }

                var drinks = await SearchDrinkFromAPI(drinkName);
                if (drinks.Count == 0)
                {
                    _consoleUI.ShowError($"No drinks found for '{drinkName}'.");
                    _consoleUI.Pause();
                    return;
                }
                var selectedDrink = AnsiConsole.Prompt(
                    new SelectionPrompt<Drink>()
                    .Title("[bold cyan]Select a drink[/]")
                    .UseConverter(drink =>
                    drink.StrDrink ?? "Unknown Drink")
                    .AddChoices(drinks));
                var drinkDetails = await GetDrinkByIdAsync(selectedDrink.IdDrink!);
                if (drinkDetails == null)
                {
                    _consoleUI.ShowError("Unable to retrieve drink details.");
                    _consoleUI.Pause();
                    return;
                }

                _favoriteDrinkRepository.IncrementViewCount(drinkDetails);
                await DisplayDrinkDetailsAsync(drinkDetails);
            }
            catch (Exception ex)
            {
                _consoleUI.ShowError(ex.Message);
                _consoleUI.Pause();
            }
        }
        private async Task DisplayDrinkDetailsAsync(Drink drink)
        {
            if (string.IsNullOrWhiteSpace(drink.StrDrinkThumb))
            {
                _consoleUI.ShowError("Drink image is not available.");
                _consoleUI.Pause();
                return;
            }

            var table = new Table
            {
                Border = TableBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Expand = true
            };
            table.AddColumn(new TableColumn("[bold cyan]Property[/]").Width(15));
            table.AddColumn(new TableColumn("[bold cyan]Details[/]"));
            table.AddRow("[bold]Name[/]",Markup.Escape(drink.StrDrink ?? "N/A"));
            table.AddRow("[bold]Category[/]",Markup.Escape(drink.StrCategory ?? "N/A"));
            table.AddRow("[bold]Alcoholic[/]",Markup.Escape(drink.StrAlcoholic ?? "N/A"));
            table.AddRow("[bold]Glass[/]",Markup.Escape(drink.StrGlass ?? "N/A"));
            table.AddRow("[bold]Instructions[/]",Markup.Escape(drink.StrInstructions ?? "N/A"));

            var panel = new Panel(table)
            {
                Header = new PanelHeader("[bold aqua]DRINK DETAILS[/]"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Padding = new Padding(1, 1),
                Expand = true
            };
  
            while (true)
            {
                AnsiConsole.Clear();
                _consoleUI.ShowHeader();
                AnsiConsole.Write(Align.Center(panel));
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]What would you like to do?[/]")
                    .AddChoices(
                        "Open Drink Image",
                        "Add to Favourites",
                        "Go Back"));
            
                switch (choice)
                {
                    case "Open Drink Image":
                        OpenDrinkImage(drink.StrDrinkThumb);
                        _consoleUI.ShowInfo("Opening drink image in your browser...");
                        _consoleUI.Pause();
                        break;
                    case "Add to Favourites":
                        _favoriteDrinkRepository.InsertFavouriteDrink(drink);
                        _consoleUI.ShowSuccess("Drink added to favourite list");
                        _consoleUI.Pause();
                        break;
                    case "Go Back":
                        return;

                }
                
            }    
        }

        private void OpenDrinkImage(string imageUrl)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                    {
                        FileName = imageUrl,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                _consoleUI.ShowError($"Unable to open drink image: {ex.Message}");
                _consoleUI.Pause();
            }
        }

        public async Task<Drink?> GetDrinkByIdAsync(string drinkId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DrinkResponse>($"lookup.php?i={Uri.EscapeDataString(drinkId)}");
                return response?.Drinks?.FirstOrDefault();
            }
            catch (HttpRequestException ex)
            {
                _consoleUI.ShowError($"Error fetching drink details: {ex.Message}");
                _consoleUI.Pause();
                return null;
            }
        }
        public async Task<List<Drink>> GetDrinksByCategoryAsync(string category)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DrinkResponse>($"filter.php?c={Uri.EscapeDataString(category)}");
                return response?.Drinks ?? new List<Drink>();
            }
            catch (HttpRequestException ex)
            {
                _consoleUI.ShowError($"Error fetching drinks by category: {ex.Message}");
                _consoleUI.Pause();
                return new List<Drink>();
            }
        }

        private async Task<List<Drink>> SearchDrinkFromAPI(string drinkName)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DrinkResponse>($"search.php?s={Uri.EscapeDataString(drinkName)}");
                return response?.Drinks ?? new List<Drink>();
            }
            catch (HttpRequestException ex)
            {
                _consoleUI.ShowError(ex.Message);
                _consoleUI.Pause();
                return new List<Drink>();
            }
        }
        private async Task<List<string>> GetCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CategoryResponse>("list.php?c=list");
                return response?.Drinks
                    ?.Select(x => x.StrCategory)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList()
                    ?? new List<string>();
            }
            catch (HttpRequestException ex)
            {
                _consoleUI.ShowError($"Error fetching categories: {ex.Message}");
                _consoleUI.Pause();
                return new List<string>();
            }
        }
    }
}
