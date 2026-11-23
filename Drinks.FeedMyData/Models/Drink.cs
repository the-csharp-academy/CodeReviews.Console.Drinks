using System.Text.Json.Serialization;

//using Newtonsoft.Json;

namespace Drinks;

public class Drink
{
    [property: JsonPropertyName("strDrink")]
    public string? strDrink { get; set; }
    [property: JsonPropertyName("idDrink")]
    public string? idDrink { get; set; }
}

public class Drinks
{
    [property: JsonPropertyName("drinks")]
    public List<Drink>? DrinksList { get; set; }
}