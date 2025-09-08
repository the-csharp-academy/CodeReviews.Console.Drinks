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
            drinksService.GetCategories();

            Console.WriteLine("Choose Category: ");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid input, please try again: ");
                category = Console.ReadLine();
            }

            GetDrinksInput(category);
        }

        private void GetDrinksInput(string category)
        {
            drinksService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or go back to category menu by typing 0:");
            string drink = Console.ReadLine();

            if (drink == "0") GetCategoriesInput();

            while (!Validator.IsIdValid(drink))
            {
                Console.WriteLine("\nInvalid input, please try again: ");
                drink = Console.ReadLine();
            }

            drinksService.GetDrink(drink);
        }
    }
}
