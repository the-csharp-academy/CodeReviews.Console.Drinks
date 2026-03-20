using DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Interfaces;

public interface IDrinkRepository
{
    Task<Result<List<Category>>> GetCategoryListAsync();
    Task<Result<Drink>> GetDrinkDeailsByIdAsync(int id);
    Task<Result<DrinkImageResponse>> GetDrinkImageAsync(string url);
    Task<Result<List<DrinkSummary>>> GetDrinkListByCategoryNameAsync(string categoryName);
}