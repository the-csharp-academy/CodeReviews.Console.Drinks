using System.Text.Json.Serialization;
// using Newtonsoft.Json;

namespace Drinks;

public class Category
{
    public string? strCategory { get; set; }
}

public class Categories
{
    [property: JsonPropertyName("drinks")]
    public List<Category>? CategoriesList { get; set; }
}