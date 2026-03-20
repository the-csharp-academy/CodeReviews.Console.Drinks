using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Repositories;

namespace DrinksInfo.Application.ViewCount.ExistsById;

public class ViewCountExistsByIdHandler
{
    private readonly IDrinkViewCountRepository _viewCountRepository;
    public ViewCountExistsByIdHandler(IDrinkViewCountRepository viewCountRepository)
    {
        _viewCountRepository = viewCountRepository;
    }

    public async Task<Result> HandleAsync(int id)
    {
        return await _viewCountRepository.ExistsByIdAsync(id);
    }
}
