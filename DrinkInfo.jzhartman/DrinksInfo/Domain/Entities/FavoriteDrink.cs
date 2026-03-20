namespace DrinksInfo.Domain.Entities;

public class FavoriteDrink
{
    public long Id { get; set; }
    public long DrinkId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }

    public FavoriteDrink(long id, long drinkId, string name, string category)
    {
        Id = id;
        DrinkId = drinkId;
        Name = name;
        Category = category;
    }
}
