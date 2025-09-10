using CodeReviews.Console.Drinks.Constants;
using CodeReviews.Console.Drinks.Models;
using Spectre.Console;
using System.Text.Json;

namespace CodeReviews.Console.Drinks.Services;

internal class DrinkService
{
    private readonly HttpClient _httpClient;

    public DrinkService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<DrinkCategory>> GetCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(ApiConstants.CategoriesUrl);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var drinkCategoryResponse = JsonSerializer.Deserialize<DrinkCategoryResponse>(response, jsonOptions);
            return drinkCategoryResponse?.Drinks ?? new List<DrinkCategory>();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"Network error while fetching categories: {ex.Message}");
            return new List<DrinkCategory>();
        }
        catch (JsonException ex)
        {
            AnsiConsole.WriteLine($"JSON parsing error while fetching categories: {ex.Message}");
            return new List<DrinkCategory>();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Unexpected error while fetching categories: {ex.Message}");
            return new List<DrinkCategory>();
        }
    }

    public async Task<List<Drink>> GetDrinksByCategoryAsync(string drinkCategory)
    {
        if (string.IsNullOrWhiteSpace(drinkCategory))
        {
            throw new ArgumentException("Drink category cannot be null or empty", nameof(drinkCategory));
        }

        try
        {
            var response = await _httpClient.GetStringAsync(
                ApiConstants.GetDrinksByCategoryUrl(drinkCategory));

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var drinkResponse = JsonSerializer.Deserialize<DrinkResponse>(response, jsonOptions);

            return drinkResponse?.Drinks ?? new List<Drink>();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"Network error while fetching drinks for category '{drinkCategory}': {ex.Message}");
            return new List<Drink>();
        }
        catch (JsonException ex)
        {
            AnsiConsole.WriteLine($"JSON parsing error while fetching drinks for category '{drinkCategory}': {ex.Message}");
            return new List<Drink>();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Unexpected error while fetching drinks for category '{drinkCategory}': {ex.Message}");
            return new List<Drink>();
        }
    }

    public async Task<DrinkDetail?> GetDrinkDetailAsync(string drinkId)
    {
        if (string.IsNullOrWhiteSpace(drinkId))
        {
            throw new ArgumentException("Drink ID cannot be null or empty", nameof(drinkId));
        }

        try
        {
            var response = await _httpClient.GetStringAsync(
                ApiConstants.GetDrinkDetailUrl(drinkId));

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var drinkDetailResponse = JsonSerializer.Deserialize<DrinkDetailResponse>(response, jsonOptions);

            return drinkDetailResponse?.Drinks?.FirstOrDefault();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.WriteLine($"Network error while fetching drink details for ID '{drinkId}': {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            AnsiConsole.WriteLine($"JSON parsing error while fetching drink details for ID '{drinkId}': {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Unexpected error while fetching drink details for ID '{drinkId}': {ex.Message}");
            return null;
        }
    }
}
