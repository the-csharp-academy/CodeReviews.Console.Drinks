using System.Text.Json.Serialization;

namespace Drinks.Models
{
    internal class DrinkList
    {
        [JsonPropertyName("drinks")]
        public List<Drink>? DrinksList { get; set; }
    }

    internal class Drink
    {
        [JsonPropertyName("idDrink")] public string? Id { get; set; }
        [JsonPropertyName("strDrink")] public string? Name { get; set; }
    }
}
