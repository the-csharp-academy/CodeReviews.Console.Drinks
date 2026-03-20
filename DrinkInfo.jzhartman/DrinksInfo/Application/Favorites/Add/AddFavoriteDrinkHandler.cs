using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Favorites.AddFavoriteDrink;

public class AddFavoriteDrinkHandler
{
    private readonly IFavoriteDrinkRepository _favoriteDrinkRepo;

    public AddFavoriteDrinkHandler(IFavoriteDrinkRepository favoriteDrinkRepo)
    {
        _favoriteDrinkRepo = favoriteDrinkRepo;
    }

    public async Task<Result> HandleAsync(AddFavoriteCommand drink)
    {
        var favoriteExistsResult = await _favoriteDrinkRepo.ExistsByIdAsync(drink.DrinkId);

        if (favoriteExistsResult.IsSuccess)
            return Result.Failure(Errors.FavoriteExists);

        return await _favoriteDrinkRepo.AddAsync(new(0, drink.DrinkId, drink.Name, drink.Category));
    }
}
