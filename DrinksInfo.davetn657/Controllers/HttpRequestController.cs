using DrinksInfo.davetn657.Models;
using Spectre.Console;
using System.Text.Json;

namespace DrinksInfo.davetn657.Controllers;

internal class HttpRequestController
{
    private readonly HttpClient _httpClient;

    public HttpRequestController()
    {
        _httpClient = new HttpClient();
    }

    internal async Task<List<string>> GetCategoryList()
    {
        try
        {
            var responseUri = new Uri("http://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            using var response = await _httpClient.GetAsync(responseUri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categoryDetails = JsonSerializer.Deserialize<CategoryDetails>(json);

            var categories = categoryDetails.Categories;
            var categoryNames = new List<string>();

            foreach (var category in categories)
            {
                categoryNames.Add(category.Name);
            }

            return categoryNames;
        }
        catch (HttpRequestException ex)
        {
            HttpRequestErrorMessage(ex.Message);
            return new List<string>();
        }
    }

    internal async Task<List<string>> GetDrinks(string category)
    {
        try
        {
            var responseUri = new Uri($"http://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            using var response = await _httpClient.GetAsync(responseUri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var drinkDetails = JsonSerializer.Deserialize<DrinkDetails>(json);

            var drinks = drinkDetails.Drinks;
            var drinkNames = new List<string>();

            foreach(var drink in drinks)
            {
                drinkNames.Add(drink.Name);
            }

            return drinkNames;
        }
        catch (HttpRequestException ex)
        {
            HttpRequestErrorMessage(ex.Message);
            return new List<string>();
        }
    }

    internal async Task<Drink> GetDrinkDetails(string name)
    {
        try
        {
            var responseUri = new Uri($"http://www.thecocktaildb.com/api/json/v1/1/search.php?s={name}");
            using var response = await _httpClient.GetAsync(responseUri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var drinkDetails = JsonSerializer.Deserialize<DrinkDetails>(json);

            return drinkDetails.Drinks[0];

        }
        catch (HttpRequestException ex)
        {
            HttpRequestErrorMessage(ex.Message);
            return new Drink();
        }
    }

    internal async Task<byte[]>? GetImage(string uri)
    {
        try
        {
            var responseUri = new Uri(uri);
            var imageBytes = await _httpClient.GetByteArrayAsync(responseUri);

            return imageBytes;
        }
        catch (HttpRequestException ex)
        {
            HttpRequestErrorMessage(ex.Message);
        }

        return null;
    }

    private void HttpRequestErrorMessage(string ex)
    {
        AnsiConsole.Write("Could not retrieve data from API!");
        AnsiConsole.WriteLine("\nMessage Caught");
        AnsiConsole.WriteLine(ex);
    }
}