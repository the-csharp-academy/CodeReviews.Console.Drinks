namespace DrinksInfo;

using DrinksInfo.Models;
using System.Text.Json;

internal class ApiService
{
    private readonly HttpClient _httpClient = new();

    public async Task<CategoryList?> GetCategories()
    {
        const string categoriesUrl = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";
        try
        {
            string categories = await _httpClient.GetStringAsync(categoriesUrl);
            CategoryList? categoryList = JsonSerializer.Deserialize<CategoryList>(categories);
            return categoryList;
        }
        catch (System.Net.Http.HttpRequestException)
        {
            return null;
        }
    }

    public async Task<DrinkList?> GetDrinksByCategory(string chosenCategory)
    {
        string escapedCategory = Uri.EscapeDataString(chosenCategory);
        string drinksUrl = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={escapedCategory}";
        try
        {
            string drinks = await _httpClient.GetStringAsync(drinksUrl);
            DrinkList? drinksList = JsonSerializer.Deserialize<DrinkList>(drinks);
            return drinksList;
        }
        catch (System.Net.Http.HttpRequestException)
        {
            return null;
        }
    }

    public async Task<DrinkDetailsList?> GetDrinkDetails(string drinkId)
    {
        string drinkDetailsUrl = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkId}";
        try
        {
            string drinkDetails = await _httpClient.GetStringAsync(drinkDetailsUrl);
            DrinkDetailsList? drinkDetailsList = JsonSerializer.Deserialize<DrinkDetailsList>(drinkDetails);
            return drinkDetailsList;
        }
        catch (System.Net.Http.HttpRequestException)
        {
            return null;
        }
    }
}