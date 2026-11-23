using System.Reflection.Metadata;
using Spectre.Console;
using Refit;

namespace Drinks;

class Program
{
    static async Task Main(string[] args)
    {
        // UserInput userInput = new();
        // string category = userInput.GetCategoriesInput();
        // userInput.GetDrinksInput(category);

        // Console.ReadKey();


        // For simple scenarios without dependency injection

        IDrinksApi drinksApi = RestService.ForGenerated<IDrinksApi>(
            "http://www.thecocktaildb.com/api/json/v1/1",
            DrinksJsonContext.Default);

        DrinksService drinksService = new DrinksService(drinksApi);

        Categories categories = await drinksService.GetCategories();

        if (categories.CategoriesList != null)
            ChoicesPrompt.SelectOne(categories.CategoriesList, "Category", "blue");

        Console.ReadKey();
    }
}