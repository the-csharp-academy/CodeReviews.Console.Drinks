using System.Text.Json.Serialization;

namespace DrinkInfoPractice.Models;

internal class ApiResponse<T>
{
    [JsonPropertyName("drinks")]
    public List<T> Drinks { get; set; }
}
