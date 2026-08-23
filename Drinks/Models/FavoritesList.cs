using Microsoft.VisualBasic;

namespace Drinks;

internal class FavoritesList : DrinkIdName
{

    public FavoritesList(int id, string name)
    : base(id, name){}
    internal static List<FavoritesList> favoriteDrinks = new();

    internal static void CheckDouble(int id, string name)
    {
        if (favoriteDrinks.Count == 0)
        {
            FavoritesList newFavorite = new FavoritesList(id, name);
            favoriteDrinks.Add(newFavorite);
            Console.WriteLine("Drink added to favorites.");

        }
        else
        {
            foreach (FavoritesList favorite in favoriteDrinks)
            {
                if (favorite.Id.Equals(id))
                {
                    Console.WriteLine($"Drink alreay in the favorites => {name}");
                    return;
                }
            }
            FavoritesList newFavorite = new FavoritesList(id, name);
            favoriteDrinks.Add(newFavorite);
            Console.WriteLine("Drink added to favorites.");
        }
    }
}