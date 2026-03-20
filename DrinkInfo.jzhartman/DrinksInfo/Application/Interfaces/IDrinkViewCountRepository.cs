using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Infrastructure.Repositories;

public interface IDrinkViewCountRepository
{
    Task<Result> AddByDrinkIdAsync(int id);
    Task<Result> ExistsByIdAsync(int id);
    Task<Result<DrinkViewCount>> GetCountByIdAsync(int id);
    Task<Result> UpdateCountByIdAsync(int id);
}