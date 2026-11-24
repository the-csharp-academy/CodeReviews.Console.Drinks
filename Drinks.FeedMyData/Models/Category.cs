using System.Text.Json.Serialization;
// using Newtonsoft.Json;

namespace Drinks;

public record Category(string? strCategory);

public record Categories
{
    [JsonPropertyName("drinks")]
    public List<Category>? CategoriesList { get; set; }
}