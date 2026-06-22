using DrinksInfo.Model;
using System.Text.Json;

namespace DrinksInfo.Service;
internal class CocktailService
{

    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/"),
    };

    public async Task<List<Category>> GetAllCategories()
    {
        try
        {
            var response = await sharedClient.GetAsync("list.php?c=list");

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            CategoryList result = JsonSerializer.Deserialize<CategoryList>(json);

            return result.categories;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }

        return new List<Category>();
    }

    public async Task<List<Drink>> GetAllDrinks(string category) 
    {
        try
        {
            var response = await sharedClient.GetAsync($"filter.php?c={category}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("drinks", out var drinksElement) 
                || drinksElement.ValueKind != JsonValueKind.Array)
            {
                return new List<Drink>();
            }

            var drinksJson = drinksElement.GetRawText();
            return JsonSerializer.Deserialize<List<Drink>>(drinksJson) ?? new List<Drink>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }

        return new List<Drink>();
    }

    public async Task<DrinkIDetail> GetDrinkInfo(string idDrink) 
    {
        try
        {
            var response = await sharedClient.GetAsync($"lookup.php?i={idDrink}");

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            DrinkDetailList result = JsonSerializer.Deserialize<DrinkDetailList>(json);

            return result?.drinkDetails?.FirstOrDefault();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }

        return new DrinkIDetail();
    }
}
