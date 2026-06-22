using System.Text.Json.Serialization;

namespace DrinksInfo.Model;
internal class Drink
{
    public string idDrink {  get; set; }
    public string strDrink { get; set; }
}

internal class DrinkList
{
    [JsonPropertyName("drinks")]
    public List<Drink> drinks { get; set; }
}