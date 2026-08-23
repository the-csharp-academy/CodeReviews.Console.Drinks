using Newtonsoft.Json;

namespace Drinks;

public class Drink
{
    public string? idDrink { get; set; }
    public string? strDrink { get; set; }
}

public class Drinks
{
    [JsonProperty("drinks")]
    public List <Drink>? drinksList { get; set; }
}