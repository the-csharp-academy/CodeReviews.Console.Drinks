namespace Drinks;

internal abstract class DrinkIdName
{
    public string Name { get; set; }
    public int Id { get; set; }

    protected DrinkIdName(int id, string name)
    {
        Id = id;
        Name = name;
    }
}