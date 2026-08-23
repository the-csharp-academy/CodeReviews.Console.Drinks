using Spectre.Console;
using static Drinks.Enums;

namespace Drinks;

class Program
{

    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                UserInput userInput = new();

                MenuActions choice = userInput.Menu();

                switch (choice)
                {
                    case MenuActions.SaveFavorite:
                        userInput.GetCategoriesInput(choice);
                        break;

                    case MenuActions.ViewFavorites:
                        TableVisualizationEngine.ShowTable(FavoritesList.favoriteDrinks, "Favorite drinks:");
                        break;

                    case MenuActions.OrderDrink:
                        userInput.GetCategoriesInput(choice);
                        break;

                    case MenuActions.ViewsCount:
                        ViewsList.PrintList();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}