// using Spectre.Console;

// namespace Drinks;

// public class UserInput
// {
//     private DrinksService _drinksService = new();

//     public string GetCategoriesInput()
//     {
//         string choice = "";
//         List<Category> categories = _drinksService.GetCategories();

//         while (true)
//         {
//             choice = AnsiConsole.Ask<string>("Choose a category:");

//             if (!categories.Any(x => x.strCategory == choice))
//                 AnsiConsole.MarkupLine("[red]Category does not exist.[/]");
//             else
//                 break;
//         }
//         _drinksService.GetDrinksPerCategories(choice);
//         return choice;
//     }

//     public void GetDrinksInput(string category)
//     {
//         string choice = "";
//         List<Drink> drinks = _drinksService.GetDrinksPerCategories(category);

//         while (true)
//         {
//             choice = AnsiConsole.Ask<string>("Choose a drink:");

//             if (!drinks.Any(x => x.strDrink == choice))
//                 AnsiConsole.MarkupLine("[red]Drink does not exist.[/]");
//             else
//                 break;
//         }

//         var selectedDrink = drinks.First(d => d.strDrink == choice);
//         _drinksService.GetDrink(selectedDrink.idDrink);
//     }
// }