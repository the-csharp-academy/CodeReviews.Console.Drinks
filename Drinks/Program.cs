using Drinks.Services;

namespace Drinks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
            client.Timeout = TimeSpan.FromSeconds(10);
            DrinksService.SetClient(client);

            await UIController.MainMenu();
        }
    }
}
