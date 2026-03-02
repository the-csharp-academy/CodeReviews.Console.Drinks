using DrinksInfo.davetn657.Controllers;
using DrinksInfo.davetn657.Models;
using Spectre.Console;

namespace DrinksInfo.davetn657.Views;

internal class UserInterface
{
    private readonly HttpRequestController _httpController;
    private readonly FavouriteDrinkController _favouriteDrinkController;
    private readonly ViewedDrinkController _viewedDrinkController;

    public UserInterface()
    {
        _httpController = new HttpRequestController();
        _favouriteDrinkController = new FavouriteDrinkController();
        _viewedDrinkController = new ViewedDrinkController();
    }

    internal void StartApplication()
    {
        while (true)
        {
            TitleCard("Welcome!");

            var menuOptions = new List<string>()
            {
                "See Favourites",
                "Search by Categories",
                "Exit"
            };

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices(menuOptions)
                .WrapAround());

            switch (selected)
            {
                case "See Favourites":
                    ListFavourites();
                    break;
                case "Search by Categories":
                    ListCategories();
                    break;
                default:
                    return;
            }
        }
    }

    private void ListFavourites()
    {
        while (true)
        {
            TitleCard("Your Favourites");

            var menuOptions = new List<string>();

            foreach(var drink in _favouriteDrinkController.GetAllFavourites())
            {
                menuOptions.Add(drink.Name);
            }
            menuOptions.Add("Return");

            if (menuOptions.Count < 0)
            {
                AnsiConsole.Prompt(new TextPrompt<string>("No favourites found!").AllowEmpty());
                return;
            }
            else
            {
                var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

                if (selected == "Return") return;

                DrinkDetails(selected);
            }
        }
    }

    private void ListCategories()
    {
        while (true)
        {
            TitleCard("Welcome!");

            var menuOptions = _httpController.GetCategoryList().Result;
            menuOptions.Add("Return");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices(menuOptions)
                .PageSize(10)
                .WrapAround()
                .EnableSearch()
                .SearchPlaceholderText("Type to search drink categories"));

            if (selected == "Return") return;

            ListDrinksByCategory(selected);
        }
    }
    private void ListDrinksByCategory(string category)
    {
        while (true)
        {
            TitleCard("Choose a Drink");

            var menuOptions = _httpController.GetDrinks(category).Result;
            menuOptions.Add("Return");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices(menuOptions)
                .PageSize(10)
                .WrapAround()
                .EnableSearch()
                .SearchPlaceholderText("Type to search drinks"));

            if (selected == "Return") return;

            DrinkDetails(selected);
        }
    }

    private void DrinkDetails(string drink)
    {
        TitleCard($"{drink}");

        var drinkDetails = _httpController.GetDrinkDetails(drink).Result;

        var imageBytes = _httpController.GetImage(drinkDetails.ImageSource).Result;

        var drinkDto = new DrinkDto()
        {
            Id = Int32.Parse(drinkDetails.DrinkId),
            Name = drinkDetails.Name
        };

        _viewedDrinkController.AddDrink(drinkDto);

        if (imageBytes != null)
        {
            var image = new CanvasImage(imageBytes)
                .MaxWidth(20)
                .BicubicResampler();

            AnsiConsole.Write(image);
        }

        var type = drinkDetails.GetType();
        var properties = type.GetProperties();

        var table = new Table();
        var tableDetails = new List<string>();

        foreach (var property in properties)
        {
            if (property.GetValue(drinkDetails) == null || property.Name == "Instructions" || property.Name == "ImageSource") continue;

            table.AddColumn(property.Name);
            tableDetails.Add(property.GetValue(drinkDetails).ToString());
        }
        table.AddRow(tableDetails.ToArray());
        
        AnsiConsole.Write($"\nInstructions: {drinkDetails.Instructions}\n" +
                          $"Times viewed before: {_viewedDrinkController.GetViewCount(drinkDto)}\n");
        AnsiConsole.Write(table);

        var checkIfFavourited = _favouriteDrinkController.CheckFavourite(drinkDto);
        var menuOptions = new List<string>();

        if (checkIfFavourited) menuOptions.Add("Add to Favourites");
        else menuOptions.Add("Remove from Favourites");

        menuOptions.Add("Return");

        var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        if(selected == "Add to Favourites")
        {
            _favouriteDrinkController.AddFavourite(drinkDto);
            AnsiConsole.Prompt(new TextPrompt<string>($"{drink} added to favourites!").AllowEmpty());
        }
        else if (selected == "Remove from Favourites")
        {
            _favouriteDrinkController.RemoveFavourite(drinkDto);
            AnsiConsole.Prompt(new TextPrompt<string>($"{drink} removed from favourites!").AllowEmpty());
        }
    }

    private void TitleCard(string title)
    {
        var titleCard = new FigletText(title)
        {
            Justification = Justify.Center,
            Color = Color.Green
        };

        var border = new Panel(titleCard)
        {
            Border = BoxBorder.Ascii
        };

        AnsiConsole.Clear();
        AnsiConsole.Write(border);
    }
}