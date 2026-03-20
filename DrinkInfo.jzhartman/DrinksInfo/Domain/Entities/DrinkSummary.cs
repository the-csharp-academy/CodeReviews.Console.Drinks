namespace DrinksInfo.Domain.Entities;

public class DrinkSummary
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Category { get; set; }

    public DrinkSummary(int id, string name, string imageUrl, string category)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Category = category;
    }
}
