using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Spectre.Console;
using static Drinks.Enums;

namespace Drinks;

public class UserInput
{
     MenuActions choice;

    internal MenuActions Menu()
    {
        choice = AnsiConsole.Prompt(
            new SelectionPrompt<MenuActions>()
            .Title("What do you want to do next?")
            .AddChoices(Enum.GetValues<MenuActions>())
        );

        return choice;
    }

    internal void GetCategoriesInput(MenuActions choice)
    {
        DrinkService.GetCategories();

        Console.WriteLine("Choose a category:");
        string? category = Console.ReadLine();

        while (!Validator.IsStringValid(category!))
        {
            Console.WriteLine("Invalid Input.");
            category = Console.ReadLine();
        }

        GetDrinkInput(category!, choice);
    }

    internal void GetDrinkInput(string category, MenuActions choice)
    {
        DrinkService.GetDrinksByCategory(category, choice);

        Console.WriteLine("Insert a drink ID to see ingredients: (Insert 0 to get back to Categories list)");
        string? drink = Console.ReadLine();

        if (drink == "0")
        {
            GetCategoriesInput(choice);
            return;
        }

        while (!Validator.IsIdValid(drink!))
        {
            Console.WriteLine("Invalid input.");
            drink = Console.ReadLine();
        }
        DrinkService.GetDrinkDetails(drink!, choice);
    }
}