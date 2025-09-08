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
            throw new NotImplementedException();
        }
    }
}
