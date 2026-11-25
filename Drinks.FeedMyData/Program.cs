using Spectre.Console;
using Refit;


namespace Drinks;

class Program
{
    static async Task Main(string[] args)
    {
        IDrinksApi drinksApi = RestService.ForGenerated<IDrinksApi>(
            "http://www.thecocktaildb.com/api/json/v1/1",
            DrinksJsonContext.Default);

        DrinksService drinksService = new DrinksService(drinksApi);
        UserInterface UI = new UserInterface(drinksService);

        await UI.CallMenu();
    }
}