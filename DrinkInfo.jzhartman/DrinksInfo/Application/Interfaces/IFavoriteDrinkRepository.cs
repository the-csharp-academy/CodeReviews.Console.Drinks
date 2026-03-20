using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Interfaces;

public interface IFavoriteDrinkRepository
{
    Task<Result> AddAsync(FavoriteDrink drink);
    Task<Result> DeleteByIdAsync(int id);
    Task<Result> ExistsByIdAsync(int id);
    Task<Result<List<FavoriteDrink>>> GetAllAsync();
}