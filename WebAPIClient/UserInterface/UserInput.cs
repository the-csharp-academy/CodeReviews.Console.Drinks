using Spectre.Console;
using WebAPIClient.Services;
using WebAPIClient.Validation;

namespace WebAPIClient.UserInterface
{
    internal class UserInput
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly DrinksService drinksService = new(_httpClient);
        internal async Task GetCategoriesInput()
        {
            try
            {
                var categoryList = await drinksService.GetCategories();

                string category = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                                    .Title("Choose [green]category:[/]")
                                                                    .AddChoices(categoryList.Select(c => c.strCategory)
                                                                    .Append("Exit")));
                if (category == "Exit")
                {
                    return;
                }
                else
                {
                    if (!Validator.IsStringValid(category))
                    {
                        throw new Exception("\nInvalid category");
                    }

                    await GetDrinksInput(category);
                }
            }
            catch(Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        private async Task GetDrinksInput(string category)
        {
            try
            {
                var drinkList = await drinksService.GetDrinksByCategory(category);

                string drink = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                                    .Title("Choose [green]drink:[/]")
                                                                    .AddChoices(drinkList.Select(c => c.strDrink)
                                                                    .Append("Back")));

                if (drink == "Back")
                {
                    await GetCategoriesInput();
                }
                else
                {
                    var drinkId = drinkList.Where(d => d.strDrink == drink)
                                           .Select(d => d.idDrink)
                                           .FirstOrDefault()
                                           ?.ToString();

                    while (!Validator.IsIdValid(drinkId))
                    {
                        throw new Exception("\nInvalid drink");
                    }

                    await drinksService.GetDrink(drinkId);

                    string choise = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                                    .Title("What to do [green]next:[/]")
                                                                    .AddChoices("Back to category section", "Exit"));
                    if (choise == "Exit")
                    {
                        return;
                    }
                    else await GetCategoriesInput();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }
    }
}
