using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Favorites.GetAllFavoriteDrinks;

public class GetAllFavoriteDrinksHandler
{
    private readonly IFavoriteDrinkRepository _favoriteRepo;

    public GetAllFavoriteDrinksHandler(IFavoriteDrinkRepository favoriteRepo)
    {
        _favoriteRepo = favoriteRepo;
    }

    public async Task<Result<List<FavoriteDrinkResponse>>> HandleAsync()
    {
        var result = await _favoriteRepo.GetAllAsync();

        if (result.IsFailure)
            return Result<List<FavoriteDrinkResponse>>.Failure(result.Errors.Prepend(Errors.GetAllFavoriteDrinksFailed));
        if (result?.Value is null)
            return Result<List<FavoriteDrinkResponse>>.Failure([Errors.GetAllFavoriteDrinksFailed, Errors.GenericNull]);
        if (result.Value.Count == 0)
            return Result<List<FavoriteDrinkResponse>>.Failure(Errors.FavoriteListEmpty);
        else
            return Result<List<FavoriteDrinkResponse>>.Success(await MapToResponseAsync(result.Value));
    }

    private async Task<List<FavoriteDrinkResponse>> MapToResponseAsync(List<FavoriteDrink> favorites)
    {
        var output = new List<FavoriteDrinkResponse>();

        foreach (var drink in favorites)
        {
            output.Add(new FavoriteDrinkResponse((int)drink.Id, (int)drink.DrinkId, drink.Name, drink.Category));
        }
        return output;
    }
}
