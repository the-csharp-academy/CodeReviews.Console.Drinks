using System.Text.Json.Serialization;

namespace Drinks.Models
{
    internal class CategoryList
    {
        [JsonPropertyName("drinks")]
        public List<Category>? CategoriesList { get; set; }
    }

    internal class Category
    {
        [JsonPropertyName("strCategory")] public string? Name { get; set; }
    }
}