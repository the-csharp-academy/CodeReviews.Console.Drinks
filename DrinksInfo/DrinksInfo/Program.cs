using DrinksInfo.Models;
using Spectre.Console;

namespace DrinksInfo;

internal static class Program
{
    private static List<string> favoriteDrinks = new List<string>();
    private static Dictionary<string, int> drinkViewCounts = new();

    private static async Task Main()
    {
        bool isRunning = true;

        ApiService api = new ApiService();
        while (isRunning)
        {
            string mainMenuChoice = MainMenu();
            switch (mainMenuChoice)
            {
                case "Choose categories":
                    CategoryList? categories = await api.GetCategories();
                    if (categories == null)
                    {
                        Console.WriteLine("Error! Categories is null.");
                        return;
                    }
                    string chosenCategory = CategoryMenu(categories);
                    DrinkList? drinks = await api.GetDrinksByCategory(chosenCategory);
                    if (drinks == null)
                    {
                        Console.WriteLine("Error! Drinks is null.");
                        return;
                    }
                    Drink chosenDrink = DrinksMenu(drinks);
                    if (chosenDrink == null)
                    {
                        Console.WriteLine("Error! Drink data is null");
                        return;
                    }
                    MostPopularDrinksCount(chosenDrink);

                    string drinkOptionChoice = DrinkOptionsMenu(chosenDrink);
                    if (drinkOptionChoice == "Add to Favorite")
                    {
                        FavoriteOption(chosenDrink);
                        ExitToMainMenu();
                    }
                    else
                    {
                        DrinkDetailsList? drinkDetails = await api.GetDrinkDetails(chosenDrink.idDrink);
                        if (drinkDetails == null)
                        {
                            Console.WriteLine("Could not load categories.");
                            return;
                        }

                        if (drinkDetails.drinkDetails == null)
                        {
                            Console.WriteLine("Could not load categories.");
                            return;
                        }
                        if (drinkDetails.drinkDetails.Count == 0)
                        {
                            Console.WriteLine("Could not load categories.");
                            return;
                        }
                        DrinkDetails selectedDrinkDetails = drinkDetails.drinkDetails[0];
                        DrinkDetailsMenu(selectedDrinkDetails);
                        ExitToMainMenu();
                    }
                    break;

                case "Favorites":
                    FavoriteList();
                    ExitToMainMenu();
                    break;

                case "Most popular":
                    MostPopularDrinksTable();
                    ExitToMainMenu();
                    break;

                case "Exit":
                    isRunning = false;
                    break;
            }
        }
    }

    public static string MainMenu()
    {
        string MainMenu =
            AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("-------Drinks Info Program-------")
                .AddChoices("Choose categories", "Favorites", "Most popular", "Exit"));
        return (MainMenu);
    }

    public static string CategoryMenu(CategoryList categories)
    {
        List<string> categoryNames = new List<string>();
        foreach (Category category in categories.drinks)
        {
            categoryNames.Add(category.strCategory);
        }

        string chosenCategory =
            AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select drinks category")
            .PageSize(12)
            .AddChoices(categoryNames));
        return chosenCategory;
    }

    public static Drink DrinksMenu(DrinkList drinkList)
    {
        Drink chosenDrink =
           AnsiConsole.Prompt(
               new SelectionPrompt<Drink>()
               .Title("Select drink")
               .PageSize(12)
               .UseConverter(drink => drink.strDrink)
               .AddChoices(drinkList.drinks));
        return chosenDrink;
    }

    public static string DrinkOptionsMenu(Drink chosenDrink)
    {
        string drinkOptionChoice =
            AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(3)
                .Title(@$"Selected drink ""{chosenDrink.strDrink}"" ")
                .AddChoices(@$"Add to Favorite", @$"See ingredients"));
        return drinkOptionChoice;
    }

    public static void DrinkDetailsMenu(DrinkDetails drinkDetails)
    {
        string?[] measureArray = { drinkDetails.strMeasure1, drinkDetails.strMeasure2, drinkDetails.strMeasure3, drinkDetails.strMeasure4, drinkDetails.strMeasure5,
            drinkDetails.strMeasure6, drinkDetails.strMeasure7, drinkDetails.strMeasure8, drinkDetails.strMeasure9, drinkDetails.strMeasure10, drinkDetails.strMeasure11,
            drinkDetails.strMeasure12, drinkDetails.strMeasure13, drinkDetails.strMeasure14, drinkDetails.strMeasure15 };

        string?[] ingredientsArray = { drinkDetails.strIngredient1, drinkDetails.strIngredient2, drinkDetails.strIngredient3, drinkDetails.strIngredient4, drinkDetails.strIngredient5,
            drinkDetails.strIngredient6, drinkDetails.strIngredient7, drinkDetails.strIngredient8, drinkDetails.strIngredient9, drinkDetails.strIngredient10, drinkDetails.strIngredient11,
            drinkDetails.strIngredient12, drinkDetails.strIngredient13, drinkDetails.strIngredient14, drinkDetails.strIngredient15 };

        var drinkDetailsMenu = new Table()
            .RoundedBorder()
            .ShowRowSeparators()
            .AddColumn("Ingredients")
            .AddColumn("Measure");

        for (int i = 0; i < ingredientsArray.Length; i++)
        {
            if (string.IsNullOrEmpty(ingredientsArray[i]))
            {
                continue;
            }
            drinkDetailsMenu.AddRow(ingredientsArray[i] ?? "", measureArray[i] ?? "");
        }
        AnsiConsole.Write(drinkDetailsMenu);
    }

    public static void FavoriteOption(Drink chosenDrink)
    {
        if (favoriteDrinks.Contains(chosenDrink.strDrink))
        {
            AnsiConsole.MarkupLine($@"Drink [bold blue]""{chosenDrink.strDrink}""[/] is already on favorites list");
            return;
        }
        favoriteDrinks.Add(chosenDrink.strDrink);

        AnsiConsole.MarkupLine($@"Drink [bold blue]""{chosenDrink.strDrink}""[/] has been added to favorites");
        AnsiConsole.WriteLine("");
    }

    public static void FavoriteList()
    {
        var favoriteDrinkTable = new Table()
            .RoundedBorder()
            .ShowRowSeparators()
            .AddColumn("Favorite Drinks");

        foreach (string drinkName in favoriteDrinks)
        {
            favoriteDrinkTable.AddRow(drinkName);
        }
        AnsiConsole.Write(favoriteDrinkTable);
    }

    public static void ExitToMainMenu()
    {
        AnsiConsole.MarkupLine("Press [bold blue]Enter[/] to continue");
        Console.ReadLine();
        Console.Clear();
    }

    public static void MostPopularDrinksCount(Drink chosenDrink)
    {
        string drinkName = chosenDrink.strDrink;
        if ((drinkViewCounts.ContainsKey(drinkName) == true))
        {
            drinkViewCounts[drinkName]++;
        }
        else
        {
            drinkViewCounts.Add(drinkName, 1);
        }
    }

    public static void MostPopularDrinksTable()
    {
        var mostPopularDrinksTable = new Table()
            .AddColumns("Drink", "Count");

        foreach (KeyValuePair<string, int> drink in drinkViewCounts.OrderByDescending(drink => drink.Value))
        {
            mostPopularDrinksTable.AddRow(drink.Key, drink.Value.ToString());
        }
        AnsiConsole.Write(mostPopularDrinksTable);
    }
}