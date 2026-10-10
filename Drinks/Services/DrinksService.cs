using Drinks.Models;
using System.Net.Http.Json;

namespace Drinks.Services
{
    internal static class DrinksService
    {
        private static HttpClient _client = null!;

        internal static void SetClient(HttpClient client) => _client = client;

        internal static async Task<List<Category>> GetCategories()
        {
            CategoryList? result = await _client.GetFromJsonAsync<CategoryList>("list.php?c=list");
            return result?.CategoriesList ?? new List<Category>();
        }

        internal static async Task<List<Drink>> GetDrinksByCategory(string name)
        {
            DrinkList? result = await _client.GetFromJsonAsync<DrinkList>($"filter.php?c={Uri.EscapeDataString(name)}");
            return result?.DrinksList ?? new List<Drink>();
        }

        internal static async Task<Dictionary<string, string?>?> GetDrinkById(string id)
        {
            DrinkDetailObject? result = await _client.GetFromJsonAsync<DrinkDetailObject>($"lookup.php?i={Uri.EscapeDataString(id)}");
            return result?.DrinkDetailList?.FirstOrDefault();
        }

        internal static async Task<byte[]?> GetImage(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            return await _client.GetByteArrayAsync(url);
        }
    }
}
