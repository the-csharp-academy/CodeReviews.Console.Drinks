using System.Text.Json.Serialization;

namespace DrinkInfoPractice.Models;

internal class DrinkCategory
{
    [JsonPropertyName("strCategory")]
    public string StrCategory { get; set; }
}
