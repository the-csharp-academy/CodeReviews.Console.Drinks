using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.DrinkInfoApi.GetDrinkDetailsById;

public class GetDrinkDetailsByIdHandler
{
    private readonly IDrinkRepository _drinkRepo;
    public GetDrinkDetailsByIdHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<Result<DrinkDetailResponse>> HandleAsync(int id)
    {
        var result = await _drinkRepo.GetDrinkDeailsByIdAsync(id);

        if (result.IsFailure)
            return Result<DrinkDetailResponse>.Failure(result.Errors.Prepend(Errors.GetDrinkDetailsFailed));
        if (result?.Value is null)
            return Result<DrinkDetailResponse>.Failure([Errors.GetDrinkDetailsFailed, Errors.GenericNull]);
        else
        {
            var drinkDetailResponse = new DrinkDetailResponse(
                    result.Value.Summary.Id,
                    result.Value.Summary.Name,
                    result.Value.Summary.ImageUrl,
                    result.Value.Summary.Category,
                    result.Value.IsAlcoholic,
                    result.Value.Glass,
                    result.Value.Instructions,
                    result.Value.Ingredients,
                    result.Value.Measurements);

            return Result<DrinkDetailResponse>.Success(drinkDetailResponse);
        }
    }
}
