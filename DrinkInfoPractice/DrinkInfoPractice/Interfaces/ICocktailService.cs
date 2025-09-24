using DrinkInfoPractice.Models;

namespace DrinkInfoPractice.Interfaces;

internal interface ICocktailService
{
    Task<List<DrinkCategory>> GetCategoriesAsync();
    Task<List<DrinkSummary>> GetDrinksByCategoryAsync(string category);
    Task<DrinkDetail> GetDrinkByIdAsync(string id);
}
