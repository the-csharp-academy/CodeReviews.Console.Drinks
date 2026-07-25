using System.Text.Json.Serialization;

namespace DrinkExplorer.Models
{
    public class CategoryResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category>? Drinks { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("strCategory")]
        public string? StrCategory { get; set; }
    }
}
