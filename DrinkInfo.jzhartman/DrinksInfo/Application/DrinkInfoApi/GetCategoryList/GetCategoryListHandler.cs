using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.DrinkInfoApi.GetCategoryList;

public class GetCategoryListHandler
{
    private readonly IDrinkRepository _drinkRepo;

    public GetCategoryListHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<Result<List<CategoryResponse>>> HandleAsync()
    {
        var result = await _drinkRepo.GetCategoryListAsync();

        if (result.IsFailure)
            return Result<List<CategoryResponse>>.Failure(result.Errors.Prepend(Errors.GetCategoryFailed));
        if (result?.Value == null)
            return Result<List<CategoryResponse>>.Failure([Errors.GetCategoryFailed, Errors.GenericNull]);
        else
            return Result<List<CategoryResponse>>.Success(await MapToResponseAsync(result.Value));
    }

    private static async Task<List<CategoryResponse>> MapToResponseAsync(List<Category> categories)
    {
        var output = new List<CategoryResponse>();

        foreach (var category in categories)
        {
            output.Add(new CategoryResponse(category.Name));
        }
        return output;
    }
}
