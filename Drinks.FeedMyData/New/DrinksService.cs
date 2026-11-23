using System.Text.Json.Serialization;
using Spectre.Console;
// using Newtonsoft.Json;
// using RestSharp;
using System.Web;
using Refit;

namespace Drinks;

public class DrinksService
{
    private readonly IDrinksApi _drinksApi;

    public DrinksService(IDrinksApi drinksApi)
    {
        _drinksApi = drinksApi;
    }

    public async Task<Categories> GetCategories()
    {
        Categories categories = await _drinksApi.GetCategoriesAsync();

        if (categories == null)
            return new Categories();

        else
            return categories;
    }

    public async Task<List<Drink>> GetDrinksPerCategories(string category)
    {
        return await _drinksApi.GetDrinksPerCategoryAsync(category);
    }
}