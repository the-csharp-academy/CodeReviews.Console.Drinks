using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInformation.JJHH17
{
    internal class UserInput
    {
        DrinksService drinksService = new DrinksService();

        internal void GetCategoriesInput()
        {
            var categories = drinksService.GetCategories();

            Console.WriteLine("Choose Category: ");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid input, please try again: ");
                category = Console.ReadLine();
            }

            if (!categories.Any(x => x.strCategory == category))
            {
                Console.WriteLine("\nCategory does not exist, please try again: ");
                GetCategoriesInput();
            }

            GetDrinksInput(category);
        }

        private void GetDrinksInput(string category)
        {
            var drinks = drinksService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or go back to category menu by typing 0:");
            string drink = Console.ReadLine();

            if (drink == "0") GetCategoriesInput();

            while (!Validator.IsIdValid(drink))
            {
                Console.WriteLine("\nInvalid input, please try again: ");
                drink = Console.ReadLine();
            }

            if (!drinks.Any(x => x.idDrink == drink))
            {
                Console.WriteLine("\nDrink does not exist, please try again: ");
                GetDrinksInput(category);
            }

            drinksService.GetDrink(drink);

            Console.WriteLine("Press any key to go back to category menu...");
            Console.ReadKey();
            if (!Console.KeyAvailable) GetCategoriesInput();
        }
    }
}
