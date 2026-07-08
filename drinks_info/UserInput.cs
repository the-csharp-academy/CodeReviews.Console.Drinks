using drinks_info.Service;
using System;

namespace drinks_info
{
    public class UserInput
    {
        DrinksService drinksService = new();
        FavouriteService favouriteService = new();
        DrinkViewService drinkViewService = new();

        internal void GetMenuInput()
        {
            Console.Clear();
            Console.WriteLine("\n\nMENU\n\n");
            Console.WriteLine("\t0 | Exit.");
            Console.WriteLine("\t1 | Get category list.");
            Console.WriteLine("\t2 | Get favorite drink list.");
            Console.WriteLine("\t3 | Get drink views count.");
            Console.Write("\n\nEnter a number to choose an action:\t");
            string? inp = Console.ReadLine();
            while(!Validator.IsChoiceValid(inp))
            {
                Console.Write("\nPlease enter a valid number!\nTry again:\t");
                inp = Console.ReadLine();
            }
            int choice = int.Parse(inp);

            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    GetCategoriesInput();
                    break;
                case 2:
                    favouriteService.GetAll();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                case 3:
                    drinkViewService.GetAll();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                default:
                    break;
            }
        }

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

            var drinkDetail = drinksService.GetDrink(drink);
            drinkViewService.IncreaseViewCount(drinkDetail.idDrink, drinkDetail.strDrink);

            Console.WriteLine("\nDo you want to add this drink to favourite list? (Y/N)");
            Console.WriteLine("Press 'Y' to add this drink to favourite.");
            Console.WriteLine("Press any other key to go back to menu.");
            Console.Write("Your choice:\t");
   
            if(Console.ReadKey().Key == ConsoleKey.Y)
            {
                favouriteService.Add(drinkDetail.idDrink);
                Console.WriteLine("\nAdd success!");
                Console.WriteLine("Press any key to go back to menu.");
                Console.ReadKey();
            }
            
            GetMenuInput();
        }

    }
}
