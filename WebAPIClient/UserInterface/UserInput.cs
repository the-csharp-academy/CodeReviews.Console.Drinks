using WebAPIClient.Services;
using WebAPIClient.Validation;

namespace WebAPIClient.UserInterface
{
    internal class UserInput
    {
        DrinksService drinksService = new();

        internal async Task GetCategoriesInput()
        {
            await drinksService.GetCategories();

            
            Console.WriteLine("Choose category:");

            string category = Console.ReadLine();

            while(!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid category");
                category = Console.ReadLine();
            }

            await GetDrinksInput(category);
        }

        private async Task GetDrinksInput(string category)
        {
            await drinksService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or type 0 to go back:");

            string drink = Console.ReadLine();

            if (drink == "0") GetCategoriesInput();

            while(!Validator.IsIdValid(drink))
            {
                Console.WriteLine("\nInvalid drink");
                drink = Console.ReadLine();
            }

            await drinksService.GetDrink(drink);
        }
    }
}
