using System.Text.Json.Serialization;

//using Newtonsoft.Json;

namespace Drinks;

public record Drink(string? strDrink, string? idDrink);

public record Drinks
{
    [property: JsonPropertyName("drinks")]
    public List<Drink>? DrinksList { get; set; }
}