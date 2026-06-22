using System.Text.Json.Serialization;

namespace DrinksInfo.Model;
internal class Category
{
    public string strCategory {  get; set; }
}

internal class CategoryList
{
    [JsonPropertyName("drinks")]
    public List<Category> categories { get; set; }
}
