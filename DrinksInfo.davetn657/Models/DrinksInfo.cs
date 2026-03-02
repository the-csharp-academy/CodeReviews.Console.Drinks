using System.Text.Json.Serialization;

namespace DrinksInfo.davetn657.Models;

public record class DrinkDetails()
{
    [JsonPropertyName("drinks")]
    public List<Drink>? Drinks { get; set; }
}

public record class Drink()
{
    [JsonPropertyName("idDrink")]
    public string DrinkId { get; set; }
    [JsonPropertyName("strDrink")]
    public string? Name { get; set; }
    [JsonPropertyName("strCategory")]
    public string? Category { get; set; }
    [JsonPropertyName("strIBA")]
    public string? Iba { get; set; }
    [JsonPropertyName("strAlcoholic")]
    public string? IsAlcoholic { get; set; }
    [JsonPropertyName("strInstructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("strDrinkThumb")]
    public string? ImageSource { get; set; }

    [JsonPropertyName("strIngredient1")]
    public string? Ingredient1 { get; set; }
    [JsonPropertyName("strMeasure1")]
    public string? Measurement1 { get; set; }

    [JsonPropertyName("strIngredient2")]
    public string? Ingredient2 { get; set; }
    [JsonPropertyName("strMeasure2")]
    public string? Measurement2 { get; set; }

    [JsonPropertyName("strIngredient3")]
    public string? Ingredient3 { get; set; }
    [JsonPropertyName("strMeasure3")]
    public string? Measurement3 { get; set; }

    [JsonPropertyName("strIngredient4")]
    public string? Ingredient4 { get; set; }
    [JsonPropertyName("strMeasure4")]
    public string? Measurement4 { get; set; }

    [JsonPropertyName("strIngredient5")]
    public string? Ingredient5 { get; set; }
    [JsonPropertyName("strMeasure5")]
    public string? Measurement5 { get; set; }

    [JsonPropertyName("strIngredient6")]
    public string? Ingredient6 { get; set; }
    [JsonPropertyName("strMeasure6")]
    public string? Measurement6 { get; set; }

    [JsonPropertyName("strIngredient7")]
    public string? Ingredient7 { get; set; }
    [JsonPropertyName("strMeasure7")]
    public string? Measurement7 { get; set; }

    [JsonPropertyName("strIngredient8")]
    public string? Ingredient8 { get; set; }
    [JsonPropertyName("strMeasure8")]
    public string? Measurement8 { get; set; }

    [JsonPropertyName("strIngredient9")]
    public string? Ingredient9 { get; set; }
    [JsonPropertyName("strMeasure9")]
    public string? Measurement9 { get; set; }

    [JsonPropertyName("strIngredient10")]
    public string? Ingredient10 { get; set; }
    [JsonPropertyName("strMeasure10")]
    public string? Measurement10 { get; set; }

    [JsonPropertyName("strIngredient11")]
    public string? Ingredient11 { get; set; }
    [JsonPropertyName("strMeasure11")]
    public string? Measurement11 { get; set; }

    [JsonPropertyName("strIngredient12")]
    public string? Ingredient12 { get; set; }
    [JsonPropertyName("strMeasure12")]
    public string? Measurement12 { get; set; }

    [JsonPropertyName("strIngredient13")]
    public string? Ingredient13 { get; set; }
    [JsonPropertyName("strMeasure13")]
    public string? Measurement13 { get; set; }

    [JsonPropertyName("strIngredient14")]
    public string? Ingredient14 { get; set; }
    [JsonPropertyName("strMeasure14")]
    public string? Measurement14 { get; set; }

    [JsonPropertyName("strIngredient15")]
    public string? Ingredient15 { get; set; }
    [JsonPropertyName("strMeasure15")]
    public string? Measurement15 { get; set; }
}