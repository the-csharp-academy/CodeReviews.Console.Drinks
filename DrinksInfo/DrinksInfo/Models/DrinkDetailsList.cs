using System.Text.Json.Serialization;

namespace DrinksInfo.Models
{
    internal class DrinkDetailsList
    {
        [JsonPropertyName("drinks")]
        public List<DrinkDetails> drinkDetails { get; set; }
    }
}