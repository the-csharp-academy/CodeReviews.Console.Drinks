using DrinkInfoPractice.Interfaces;
using DrinkInfoPractice.Models;
using System.Text.Json;


namespace DrinkInfoPractice.Services;

internal class CocktailService : ICocktailService
{
    // filed for stroing HttpClient
    private readonly HttpClient _httpClient;

    // API's base url
    private const string BaseUrl = "http://www.thecocktaildb.com/api/json/v1/1/";

    public CocktailService()
    {
        _httpClient = new HttpClient();

        // setting the base address
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<DrinkCategory>> GetCategoriesAsync()
    {
        try
        {
            // making the API call
            var response = await _httpClient.GetAsync("list.php?c=list");


            // check to see if call was successful
            response.EnsureSuccessStatusCode();

            // read the response content
            var content = await response.Content.ReadAsStringAsync();


            // convert the JSON to our C# objects
            // takes the json and creates DrinkCategory object
            var result = JsonSerializer.Deserialize<ApiResponse<DrinkCategory>>(content);


            //return list of categories
            // if result or Drinks is null, return empty list
            return result?.Drinks ?? new List<DrinkCategory>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting categories: {ex.Message}");
            throw;
        }
    }

    public async Task<List<DrinkSummary>> GetDrinksByCategoryAsync(string category)
    {
        try
        {
            // API call
            var response = await _httpClient.GetAsync($"filter.php?c={category}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<DrinkSummary>>(content);

            return result?.Drinks ?? new List<DrinkSummary>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting drink summary: {ex.Message}");
            throw;
        }
    }

    public async Task<DrinkDetail> GetDrinkByIdAsync(string id)
    {
        try
        {
            // API call
            var response = await _httpClient.GetAsync($"lookup.php?i={id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<DrinkDetail>>(content);

            var drink = result?.Drinks?.FirstOrDefault();

            if (drink != null)
            {
                // Then process ingredients from the JSON directly
                using (JsonDocument document = JsonDocument.Parse(content))
                {
                    var drinkElement = document.RootElement.GetProperty("drinks")[0];

                    // Process ingredients
                    for (int i = 1; i <= 15; i++)
                    {
                        if (drinkElement.TryGetProperty($"strIngredient{i}", out JsonElement ingredientElement) &&
                            ingredientElement.ValueKind != JsonValueKind.Null)
                        {
                            var ingredient = ingredientElement.GetString();
                            var measure = "As needed";

                            if (drinkElement.TryGetProperty($"strMeasure{i}", out JsonElement measureElement) &&
                                measureElement.ValueKind != JsonValueKind.Null)
                            {
                                measure = measureElement.GetString();
                            }

                            drink.AddIngredient(i, ingredient, measure);
                        }
                    }
                }
            }

            return drink ?? new DrinkDetail();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting drink details: {ex.Message}");
            throw;
        }
    }
}
