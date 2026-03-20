using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Favorites.DeleteFavoriteDrink;

public class DeleteFavoriteDrinkByIdHandler
{
    private readonly IFavoriteDrinkRepository _favoriteDrinkRepo;

    public DeleteFavoriteDrinkByIdHandler(IFavoriteDrinkRepository favoriteDrinkRepo)
    {
        _favoriteDrinkRepo = favoriteDrinkRepo;
    }

    public async Task<Result> HandleAsync(int id)
    {
        var favoriteExistsResult = await _favoriteDrinkRepo.ExistsByIdAsync(id);

        if (favoriteExistsResult.IsFailure)
            return Result.Failure(Errors.FavoriteDoesNotExist);

        return await _favoriteDrinkRepo.DeleteByIdAsync(id);
    }
}
