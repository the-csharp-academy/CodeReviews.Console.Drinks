namespace CodeReviews.Console.Drinks.Constants;

internal static class ApiConstants
{
    public const string BaseUrl = "https://www.thecocktaildb.com/api/json/v1/1";
    public const string CategoriesEndpoint = "/list.php?c=list";
    public const string DrinksByCategoryEndpoint = "/filter.php?c=";
    public const string DrinkDetailEndpoint = "/lookup.php?i=";

    public static string CategoriesUrl => $"{BaseUrl}{CategoriesEndpoint}";

    public static string GetDrinksByCategoryUrl(string category)
    {
        return $"{BaseUrl}{DrinksByCategoryEndpoint}{Uri.EscapeDataString(category)}";
    }

    public static string GetDrinkDetailUrl(string drinkId)
    {
        return $"{BaseUrl}{DrinkDetailEndpoint}{drinkId}";
    }
}
