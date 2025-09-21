
using Drinks_App.Models;

namespace Drinks_App
{
    internal class UserInput
    {
        DrinksService drinksService = new();

        internal void GetCategoriesInput ()
        {
           var categories =  drinksService.GetCategories();

            Console.WriteLine("Select a Category");
             string category =  Console.ReadLine();

            while(!Validator.ValidCategory(category))
            {
                Console.WriteLine("Invalid Category");
                 category = Console.ReadLine();
            }

            if (!categories.Any(x => x.strCategory == category))
            {
                Console.WriteLine("Category doesn't exist.");
                GetCategoriesInput();
            }

            GetDrinksByCategory(category);
        }

        public void GetDrinksByCategory(string? category)
        {
           var drinks = drinksService.GetDrinks(category);

            Console.WriteLine("Choose a Drink Id or Enter 0 to return to Categories Menue");

            string drinkId = Console.ReadLine();

            if(drinkId == "0")
            {
                GetCategoriesInput();
            }
            while(!Validator.ValidateDrinkId(drinkId))
            {
                Console.WriteLine("Invalid Drink");
                drinkId = Console.ReadLine();
            }
            if (!drinks.Any(x => x.idDrink == drinkId))
            {
                Console.WriteLine("Drink doesn't exist.");
                GetDrinksByCategory(category);
            }


            drinksService.GetDrink(drinkId);
        }
    }
}
