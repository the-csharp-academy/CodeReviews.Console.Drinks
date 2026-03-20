namespace DrinksInfo.Application.Favorites.AddFavoriteDrink;

public record AddFavoriteCommand(int DrinkId, string Name, string Category);
