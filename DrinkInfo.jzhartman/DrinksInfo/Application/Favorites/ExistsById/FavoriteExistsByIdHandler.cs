using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Favorites.ExistsById;

public class FavoriteExistsByIdHandler
{
    private readonly IFavoriteDrinkRepository _favoriteRepository;

    public FavoriteExistsByIdHandler(IFavoriteDrinkRepository favoriteDrinkRepository)
    {
        _favoriteRepository = favoriteDrinkRepository;
    }

    public async Task<Result> HandleAsync(int id)
    {
        var result = await _favoriteRepository.ExistsByIdAsync(id);

        if (result.IsFailure)
            return Result.Failure(result.Errors);
        if (result is null)
            return Result.Failure([Errors.GenericNull]);
        return
            result;
    }
}
