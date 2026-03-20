using DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Models;
using SixLabors.ImageSharp;
using System.Net.Http.Json;
using System.Text.Json;

namespace DrinksInfo.Infrastructure.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly HttpClient _client;
    public DrinkRepository(HttpClient client)
    {
        _client = client;
    }
    public async Task<Result<List<Category>>> GetCategoryListAsync()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<CategoryListApiResponse>("list.php?c=list");

            if (response?.Drinks is null)
                return Result<List<Category>>.Failure(Errors.EmptyApiResponse);

            var categories = response.Drinks
                .Select(c => new Category(c.Name))
                .ToList();

            return Result<List<Category>>.Success(categories);
        }
        catch (HttpRequestException)
        {
            return Result<List<Category>>.Failure(Errors.NetworkError);
        }
        catch (TaskCanceledException)
        {
            return Result<List<Category>>.Failure(Errors.Timeout);
        }
        catch (JsonException)
        {
            return Result<List<Category>>.Failure(Errors.InvalidJson);
        }
        catch (Exception ex)
        {
            return Result<List<Category>>.Failure(new Error("Unknown", ex.Message));
        }
    }

    public async Task<Result<List<DrinkSummary>>> GetDrinkListByCategoryNameAsync(string categoryName)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<DrinkSummaryListApiResponse>($"filter.php?c={categoryName.Replace(" ", "_")}");

            if (response?.Drinks is null)
                return Result<List<DrinkSummary>>.Failure(Errors.EmptyApiResponse);

            var drinks = response.Drinks
                .Select(d => new DrinkSummary(d.idDrink, d.strDrink, d.strDrinkThumb, categoryName))
                .ToList();

            return Result<List<DrinkSummary>>.Success(drinks);
        }
        catch (HttpRequestException)
        {
            return Result<List<DrinkSummary>>.Failure(Errors.NetworkError);
        }
        catch (TaskCanceledException)
        {
            return Result<List<DrinkSummary>>.Failure(Errors.Timeout);
        }
        catch (JsonException)
        {
            return Result<List<DrinkSummary>>.Failure(Errors.InvalidJson);
        }
        catch (Exception ex)
        {
            return Result<List<DrinkSummary>>.Failure(new Error("Unknown", ex.Message));
        }
    }

    public async Task<Result<Drink>> GetDrinkDeailsByIdAsync(int id)
    {
        try
        {
            var url = $"lookup.php?i={id}";

            var response = await _client.GetFromJsonAsync<DrinkDetailsListApiResponse>(url);

            if (response is null)
                return Result<Drink>.Failure(Errors.EmptyApiResponse);

            var responseDrink = response.Drinks[0];

            var ingredients = ExtractList(responseDrink, "strIngredient", 15);
            var measurements = ExtractList(responseDrink, "strMeasure", 15);
            var isAlcoholic = (responseDrink.strAlcoholic.ToUpper() == "ALCOHOLIC") ? true : false;

            var drink = new Drink(
                    new DrinkSummary(responseDrink.idDrink, responseDrink.strDrink, responseDrink.strDrinkThumb, responseDrink.strCategory),
                    isAlcoholic,
                    responseDrink.strGlass,
                    responseDrink.strInstructions,
                    ingredients,
                    measurements);

            return Result<Drink>.Success(drink);
        }
        catch (HttpRequestException)
        {
            return Result<Drink>.Failure(Errors.NetworkError);
        }
        catch (TaskCanceledException)
        {
            return Result<Drink>.Failure(Errors.Timeout);
        }
        catch (JsonException)
        {
            return Result<Drink>.Failure(Errors.InvalidJson);
        }
        catch (Exception ex)
        {
            return Result<Drink>.Failure(new Error("Unknown", ex.Message));
        }
    }

    public async Task<Result<DrinkImageResponse>> GetDrinkImageAsync(string url)
    {
        try
        {
            using var client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync($"{url}/small");

            if (bytes is null)
                return Result<DrinkImageResponse>.Failure(Errors.EmptyApiResponse);
            else
                return Result<DrinkImageResponse>.Success(new DrinkImageResponse(bytes));
        }
        catch (HttpRequestException)
        {
            return Result<DrinkImageResponse>.Failure(Errors.NetworkError);
        }
        catch (TaskCanceledException)
        {
            return Result<DrinkImageResponse>.Failure(Errors.Timeout);
        }
        catch (Exception ex)
        {
            return Result<DrinkImageResponse>.Failure(new Error("Unknown", ex.Message));
        }
    }

    private static List<string> ExtractList(object apiResponse, string prefix, int max)
    {
        var output = new List<string>();

        for (int i = 1; i <= max; i++)
        {
            var property = apiResponse.GetType().GetProperty($"{prefix}{i}");
            var value = property?.GetValue(apiResponse) as string;

            if (!string.IsNullOrWhiteSpace(value))
                output.Add(value);
            else
            {
                output.Add("<empty>");
            }
        }
        return output;
    }
}
