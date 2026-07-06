using drinks_info.Service;
using System;

namespace drinks_info
{
    public class UserInput
    {
        DrinksService drinksService = new();

        internal void GetCategoriesInput()
        {
            var categories = drinksService.GetCategories();

            Console.Write("Choose category:\t");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid category.");
                category = Console.ReadLine();
            }

            if(!categories.Any(x => x.strCategory == category))
            {
                Console.WriteLine("\nCategory doesn't exists.");
                GetCategoriesInput();
            }

            GetDrinksInput(category);
        }

        private void GetDrinksInput(string category)
        {
            var drinks = drinksService.GetDrinksByCategory(category);

            Console.Write("Choose a drink or go back to category menu by typing 0:\t");
            string drink = Console.ReadLine();

            if (drink == "0")
                GetCategoriesInput();

            while (!Validator.IsIdValid(drink))
            {
                Console.WriteLine("\nInvalid drink.");
                drink = Console.ReadLine();
            }

            if(!drinks.Any(x => x.idDrink == drink))
            {
                Console.WriteLine("\nDrink doesn't exist.");
                GetDrinksInput(category);
            }

            drinksService.GetDrink(drink);

            Console.WriteLine("\nPress any key to go back to categories menu.");
            Console.ReadKey();
            if (!Console.KeyAvailable)
                GetCategoriesInput();
        }

    }
}
