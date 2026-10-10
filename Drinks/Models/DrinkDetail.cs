using System.Text.Json.Serialization;

namespace Drinks.Models
{
    internal class DrinkDetailObject
    {
        [JsonPropertyName("drinks")]
        public List<Dictionary<string, string?>>? DrinkDetailList { get; set; }
    }
}
