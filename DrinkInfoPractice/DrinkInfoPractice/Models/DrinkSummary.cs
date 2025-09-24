using System.Text.Json.Serialization;

namespace DrinkInfoPractice.Models;

internal class DrinkSummary
{
    [JsonPropertyName("idDrink")]
    public string IdDrink { get; set; }

    [JsonPropertyName("strDrink")]
    public string StrDrink { get; set; }

    [JsonPropertyName("strDrinkThumb")]
    public string StrDrinkThumb { get; set; }
}
