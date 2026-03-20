using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Repositories;

namespace DrinksInfo.Application.ViewCount.GetById;

public class GetViewCountByIdHandler
{
    private readonly IDrinkViewCountRepository _viewCountRepo;

    public GetViewCountByIdHandler(IDrinkViewCountRepository viewCountRepo)
    {
        _viewCountRepo = viewCountRepo;
    }

    public async Task<Result<int>> HandleAsync(int id)
    {
        var result = await _viewCountRepo.GetCountByIdAsync(id);

        if (result.IsFailure)
            return Result<int>.Failure(result.Errors.Prepend(Errors.GetViewCountFailed));
        if (result?.Value is null)
            return Result<int>.Success(1);

        return Result<int>.Success((int)result.Value.ViewCount);
    }
}
