
using System.Text.Json.Serialization;

namespace DrinksInfo.davetn657.Models;

public record class CategoryDetails() 
{
    [JsonPropertyName("drinks")]
    public List<Category> Categories { get; set; }
}

public record class Category()
{
    [JsonPropertyName("strCategory")]
    public string Name { get; set; }
}