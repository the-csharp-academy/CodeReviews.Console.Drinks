using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;

public class GetDrinkImageHandler
{
    private readonly IDrinkRepository _drinkRepo;

    public GetDrinkImageHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<Result<DrinkImageResponse>> HandleAsync(string url)
    {
        var response = await _drinkRepo.GetDrinkImageAsync(url);

        if (response.IsFailure)
            return Result<DrinkImageResponse>.Failure(response.Errors.Prepend(Errors.GetDrinkImageFailed));
        if (response is null)
            return Result<DrinkImageResponse>.Failure([Errors.GetDrinkImageFailed, Errors.GenericNull]);
        else
            return Result<DrinkImageResponse>.Success(response.Value);
    }
}
