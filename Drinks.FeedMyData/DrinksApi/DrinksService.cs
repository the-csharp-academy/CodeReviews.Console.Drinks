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
        return categories ?? new Categories();
    }

    public async Task<Drinks> GetDrinksPerCategories(string category)
    {
        Drinks drinks = await _drinksApi.GetDrinksPerCategoryAsync(category);
        return drinks ?? new Drinks();
    }

    //DrinkDetailsContainer is a list of Count == 1, therefore we can directly return the DrinkDetails object
    public async Task<DrinkDetails> GetDrinkDetails(string idDrink)
    {
        DrinkDetailsContainer details = await _drinksApi.GetDrinkDetails(idDrink);
        return details.ViewList[0] ?? new DrinkDetails();
    }
}