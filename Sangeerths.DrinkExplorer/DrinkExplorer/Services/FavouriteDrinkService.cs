using DrinkExplorer.Database;
using DrinkExplorer.Models;
using DrinkExplorer.Validation;
using Spectre.Console;

namespace DrinkExplorer.Services
{
    public class FavouriteDrinkService
    {
        private readonly FavoriteDrinkRepository _favoriteDrinkRepository;
        private readonly ConsoleUI _consoleUI;

        public FavouriteDrinkService()
        {
            _consoleUI = new ConsoleUI();
            _favoriteDrinkRepository = new FavoriteDrinkRepository();
        }

        public void ViewAllDrinks()
        {
            DrinkResponse favouriteDrinks = _favoriteDrinkRepository.ShowAllFavourites();
            if (favouriteDrinks == null || favouriteDrinks.Drinks.Count == 0)
            {
                _consoleUI.ShowError("No Favourite Drinks found");
                return;
            }

            var table = new Table
            {
                Border = TableBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Expand = true
            };
            table.AddColumn("[bold cyan]# [/]");
            table.AddColumn("[bold cyan]Drink Name[/]");
            table.AddColumn("[bold cyan]Category[/]");
            table.AddColumn("[bold cyan]Alcoholic[/]");
            table.AddColumn("[bold cyan]Glass[/]");

            for (int i = 0; i < favouriteDrinks.Drinks.Count; i++)
            {
                var drink = favouriteDrinks.Drinks[i];
                table.AddRow(
                    (i + 1).ToString(),
                    Markup.Escape(drink.StrDrink ?? "N/A"),
                    Markup.Escape(drink.StrCategory ?? "N/A"),
                    Markup.Escape(drink.StrAlcoholic ?? "N/A"),
                    Markup.Escape(drink.StrGlass ?? "N/A"));
            }

            var panel = new Panel(table)
            {
                Header = new PanelHeader(
                    "[bold aqua]MY FAVOURITE DRINKS[/]"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Padding = new Padding(1, 1)
            };

            AnsiConsole.Write(Align.Center(panel));
        }

        public void DisplayMostViewedDrink()
        {
            List<DrinkView> mostViewedDrinks = _favoriteDrinkRepository.GetMostViewedDrinks();
            if(mostViewedDrinks == null || mostViewedDrinks.Count == 0)
            {
                _consoleUI.ShowError("No drink view data found.");
                return;
            }

            var table = new Table
            {
                Border = TableBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Expand = true
            };
            table.AddColumn("[bold cyan]# [/]");
            table.AddColumn("[bold cyan]Drink Id[/]");
            table.AddColumn("[bold cyan]Drink Name[/]");
            table.AddColumn(new TableColumn("[bold cyan]View Count[/]").RightAligned());

            for (int i = 0; i < mostViewedDrinks.Count;i++) 
            {
                var drink = mostViewedDrinks[i];
                table.AddRow(
                    (i + 1).ToString(),
                    Markup.Escape(drink.DrinkId ?? "[grey]Unknown[/]"),
                    Markup.Escape(drink.DrinkName ?? "[grey]Unknown[/]"),
                    Markup.Escape(drink.ViewCount.ToString() ?? "[grey]Unknown[/]")
                );
            }

            var panel = new Panel(table)
            {
                Header = new PanelHeader(
                    "[bold aqua]MY MOST VIEWED DRINKS[/]"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Aqua),
                Padding = new Padding(1, 1)
            };

            AnsiConsole.Write(Align.Center(panel));
        }

    }
}
