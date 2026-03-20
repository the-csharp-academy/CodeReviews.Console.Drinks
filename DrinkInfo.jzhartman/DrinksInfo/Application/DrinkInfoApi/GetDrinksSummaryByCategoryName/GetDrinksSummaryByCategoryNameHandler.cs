using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;

public class GetDrinksSummaryByCategoryNameHandler
{
    private readonly IDrinkRepository _drinkRepo;
    public GetDrinksSummaryByCategoryNameHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<Result<List<DrinkSummaryResponse>>> HandleAsync(string categoryName)
    {
        var result = await _drinkRepo.GetDrinkListByCategoryNameAsync(categoryName);

        if (result.IsFailure)
            return Result<List<DrinkSummaryResponse>>.Failure(result.Errors.Prepend(Errors.GetDrinkSummaryFailed));
        if (result?.Value is null)
            return Result<List<DrinkSummaryResponse>>.Failure([Errors.GetDrinkSummaryFailed, Errors.GenericNull]);
        else
            return Result<List<DrinkSummaryResponse>>.Success(await MapToResponse(result.Value));
    }

    private static async Task<List<DrinkSummaryResponse>> MapToResponse(List<DrinkSummary> drinks)
    {
        var output = new List<DrinkSummaryResponse>();

        foreach (var drink in drinks)
        {
            output.Add(new DrinkSummaryResponse(drink.Id, drink.Name, drink.ImageUrl, drink.Category));
        }
        return output;
    }
}