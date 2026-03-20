using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Repositories;

namespace DrinksInfo.Application.ViewCount.AddByDrinkId;

public class AddViewCountByDrinkIdHandler
{
    private readonly IDrinkViewCountRepository _viewCountRepo;
    public AddViewCountByDrinkIdHandler(IDrinkViewCountRepository viewCountRepo)
    {
        _viewCountRepo = viewCountRepo;
    }

    public async Task<Result> HandleAsync(int id)
    {
        var existsResult = await _viewCountRepo.ExistsByIdAsync(id);

        if (existsResult.IsSuccess)
            return Result.Failure([Errors.AddFailed, Errors.GenericExists]);

        return await _viewCountRepo.AddByDrinkIdAsync(id);
    }
}
