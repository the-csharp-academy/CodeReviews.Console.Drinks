using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Repositories;

namespace DrinksInfo.Application.ViewCount.UpdateById;

public class UpdateViewCountByIdHandler
{
    private readonly IDrinkViewCountRepository _viewCountRepo;
    public UpdateViewCountByIdHandler(IDrinkViewCountRepository viewCountRepo)
    {
        _viewCountRepo = viewCountRepo;
    }

    public async Task<Result> HandleAsync(int id)
    {
        var result = await _viewCountRepo.UpdateCountByIdAsync(id);

        if (result.IsFailure)
            return Result.Failure(result.Errors.Prepend(Errors.UpdateFailed));
        if (result is null)
            return Result.Failure([Errors.UpdateFailed, Errors.GenericNull]);

        return result;
    }
}
