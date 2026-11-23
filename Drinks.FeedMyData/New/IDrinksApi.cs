using Refit;

namespace Drinks;

public interface IDrinksApi
{
    // <summary> Retrieves all category
    [Get("/list.php?c=list")]
    Task<Categories> GetCategoriesAsync(
    CancellationToken cancellationToken = default);

    // <summary> Retrieves all drinks, with category filter
    [Get("/filter.php?c={category}")]
    Task<List<Drink>> GetDrinksPerCategoryAsync(
    string category,
    CancellationToken cancellationToken = default);
}