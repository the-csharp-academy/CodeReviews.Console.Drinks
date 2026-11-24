using System.Reflection.Metadata;
using Spectre.Console;
using Refit;
using System.Reflection;

namespace Drinks;

class Program
{
    static async Task Main(string[] args)
    {
        IDrinksApi drinksApi = RestService.ForGenerated<IDrinksApi>(
            "http://www.thecocktaildb.com/api/json/v1/1",
            DrinksJsonContext.Default);

        DrinksService drinksService = new DrinksService(drinksApi);

        while (true)
        {
            Categories categories = await drinksService.GetCategories();

            if (categories.ViewList == null)
                break;

            Category category = ChoicesPrompt.SelectOne(categories.ViewList, "Category", "blue");

            Drinks drinks = await drinksService.GetDrinksPerCategories(category.strCategory);

            if (drinks.ViewList == null)
                break;

            Drink drink = ChoicesPrompt.SelectOne(drinks.ViewList, "Category", "blue");

            DrinkDetails drinkDetails = await drinksService.GetDrinkDetails(drink.idDrink);

            Displayer.PrintTable(PrepareDrinkDetails(drinkDetails));

        }

        Console.ReadKey();
    }

    static private List<DisplayableDrinkDetails> PrepareDrinkDetails(DrinkDetails drinkDetails)
    {
        string formattedName = "";
        string content = "";

        List<DisplayableDrinkDetails> detailsList = new();

        foreach (PropertyInfo property in typeof(DrinkDetails).GetProperties())
        {
            if (property.PropertyType != typeof(string))
                continue;

            if (property.Name.StartsWith("str"))
                formattedName = property.Name.Substring(3);
            else
                formattedName = property.Name;

            content = property.GetValue(drinkDetails) as string;

            if (!string.IsNullOrEmpty(content))
                detailsList.Add(new(formattedName, content));
        }
        return detailsList;
    }
}