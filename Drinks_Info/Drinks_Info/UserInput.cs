

using Spectre.Console;

namespace Drinks_Info
{
    internal class UserInput
    {
        DrinksService drinksService = new();
        internal void GetCategoriesInput()
        {
            
            drinksService.GetCategories();

            Console.WriteLine("Please enter what category of drinks you would like to view.");

            string cat = Console.ReadLine() ?? string.Empty;

            while (!Validation.isStringValid(cat))
            {
                AnsiConsole.MarkupLine("[maroon] The input was invalid[/]");
                cat = Console.ReadLine() ?? string.Empty;
            }
            
                GetDrinksInput(cat);
            }
            

        private void GetDrinksInput(string cat)
        {
            drinksService.GetDrinksByCategory(cat);
            AnsiConsole.MarkupLine("Please choose a drink by ID, or return to the menu by typing 0.");

            string drink = Console.ReadLine() ?? string.Empty;

            while (!Validation.isIdValid(drink))
            {
                AnsiConsole.MarkupLine("[maroon] The input was invalid[/]");
                drink = Console.ReadLine() ?? string.Empty;
            }
            drinksService.GetDrink(drink);

        }
    }
}
