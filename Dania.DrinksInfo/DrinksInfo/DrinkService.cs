using DrinksInfo.Models;
using Newtonsoft.Json;
using Spectre.Console;
using System.Web;

namespace DrinksInfo
{
    internal class DrinkService
    {
        private static readonly HttpClient client = new HttpClient
        { 
            BaseAddress = new Uri("http://www.thecocktaildb.com/api/json/v1/1/")
        };

        internal async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();

            try
            {
                var response = await client.GetAsync("list.php?c=list");

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();

                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
                categories = serialize.categoriesList;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return categories; 
        }

        internal async Task<List<Drink>> GetDrinksByCategory(string category)
        {
            List<Drink> drinks = new List<Drink>();

            try
            {
                var response = await client.GetAsync($"filter.php?c={HttpUtility.UrlEncode(category)}");

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();

                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
                drinks = serialize.drinksList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return drinks;

        }

        internal async Task<List<DrinkDetail>> GetDrink(string drink)
        {
            List<DrinkDetail> returnedList = new List<DrinkDetail>();

            try
            {
                var response = await client.GetAsync($"lookup.php?i={drink}");

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();

                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);
                returnedList = serialize.drinkDetailList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return returnedList;
        }

        internal async Task GetImage(string url)
        {
            try
            {
                byte[] imageBytes = await client.GetByteArrayAsync(new Uri(url));

                using var memoryStream = new MemoryStream(imageBytes);

                var image = new CanvasImage(memoryStream);
                image.MaxWidth(30);
                image.BilinearResampler();
                AnsiConsole.Write(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image couldn't be rendered: {ex.Message}");
            }
        }
    }
}
