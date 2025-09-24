using System.Text.Json.Serialization;

namespace DrinkInfoPractice.Models;

internal class DrinkDetail
{
    [JsonPropertyName("idDrink")]
    public string IdDrink { get; set; }

    [JsonPropertyName("strDrink")]
    public string StrDrink { get; set; }

    [JsonPropertyName("strCategory")]
    public string StrCategory { get; set; }

    [JsonPropertyName("strGlass")]
    public string StrGlass { get; set; }

    [JsonPropertyName("strInstructions")]
    public string StrInstructions { get; set; }

    private Dictionary<int, (string Ingredients, string Measure)> _ingredients = new();

    public IReadOnlyDictionary<int, (string Ingredients, string Measure)> Ingredients => _ingredients;

    public void AddIngredient(int index, string ingredient, string measure)
    {
        if (!string.IsNullOrWhiteSpace(ingredient))
        {
            _ingredients[index] = (ingredient, measure);
        }
    }

}
