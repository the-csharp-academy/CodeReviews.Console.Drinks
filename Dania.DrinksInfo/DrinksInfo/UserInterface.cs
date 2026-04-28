using DrinksInfo.Models;
using Spectre.Console;

namespace DrinksInfo
{
    internal class UserInterface
    {
        DrinkService drinkService = new DrinkService();

        internal async Task GetCategoriesInput()
        {
            Console.Clear();
            Console.WriteLine("---Welcome to Drinks Info app---");

            var categories = await drinkService.GetCategories();
            if (categories.Count <= 0)
            {
                Console.WriteLine("No categories found. Press enter to exit.");
                Console.ReadLine();
            }
            else
            {
                var table = new Table().HideHeaders();
                table.AddColumn("Categories");
                foreach (var c in categories)
                {
                    table.AddRow(c.strCategory);
                }
                AnsiConsole.Write(table);

                Console.WriteLine("Choose a category by typing their name: ");
                string category = Console.ReadLine();

                while (!Validator.IsStringValid(category))
                {
                    Console.WriteLine("\nInvalid category");
                    category = Console.ReadLine();
                }

                if (!categories.Any(x => x.strCategory == category))
                {
                    Console.WriteLine("Category doesn't exists.");
                    Console.ReadLine();
                    await GetCategoriesInput();
                }

                await GetDrinksInput(category);
            }
        }

        internal async Task GetDrinksInput(string category)
        {
            Console.Clear();
            var drinks = await drinkService.GetDrinksByCategory(category);
            if (drinks.Count <= 0)
            {
                Console.WriteLine("No drinks found. Press enter to go back to the category menu.");
                Console.ReadLine();
                await GetCategoriesInput();
            }
            else
            {
                var table = new Table().Title($"---{category.ToUpper()}---");
                table.AddColumn("ID");
                table.AddColumn("Drink");
                foreach (var d in drinks)
                {
                    table.AddRow(d.idDrink, d.strDrink);
                }
                AnsiConsole.Write(table);

                Console.WriteLine("Choose a drink by typing their id (Type 0 to go back to the category menu): ");
                string drink = Console.ReadLine();

                if (drink == "0") await GetCategoriesInput();

                while (!Validator.IsIdValid(drink))
                {
                    Console.WriteLine("\nInvalid drink");
                    drink = Console.ReadLine();
                }

                if (!drinks.Any(x => x.idDrink == drink))
                {
                    Console.WriteLine("Drink doesn't exists");
                    Console.ReadLine();
                    await GetDrinksInput(category);
                }

                await GetDrinkDetailInput(drink, category);
            }
        }

        internal async Task GetDrinkDetailInput(string drink, string category)
        {
            Console.Clear();
            var drinkList = await drinkService.GetDrink(drink);
            if (drinkList.Count <= 0)
            {
                Console.WriteLine("No drink found. Press enter to go back to the drinks menu.");
                Console.ReadLine();
                await GetDrinksInput(category);
            }
            else
            {
                DrinkDetail drinkDetail = drinkList[0];

                var table = new Table().HideHeaders().ShowRowSeparators();
                table.AddColumn("Key");
                table.AddColumn("Value");

                foreach (var detail in drinkDetail.GetDrinkDetails())
                { 
                    table.AddRow(detail.Key, detail.Value);
                }
                AnsiConsole.Write(table);

                await drinkService.GetImage(drinkDetail.strDrinkThumb);

                Console.WriteLine("\nPress Enter to return back to the categories menu");
                Console.ReadLine();
                await GetCategoriesInput();
            }

        }

    }
}
