namespace DrinksInfo.Domain.Entities;

public class Drink
{
    public DrinkSummary Summary { get; set; }
    public bool IsAlcoholic { get; set; }
    public bool IsNonAlcoholic => !IsAlcoholic;
    public string Glass { get; set; }
    public string Instructions { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Measurements { get; set; }

    public Drink(DrinkSummary summary, bool isAlcoholic, string glass, string instructions,
        List<string> ingredients, List<string> measurements)
    {
        Summary = summary;
        IsAlcoholic = isAlcoholic;
        Glass = glass;
        Instructions = instructions;
        Ingredients = ingredients;
        Measurements = measurements;
    }
}
