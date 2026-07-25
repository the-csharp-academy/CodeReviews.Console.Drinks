using DrinkExplorer.Services;
using DrinkExplorer.Validation;
using Spectre.Console;

namespace DrinkExplorer.Controller
{
    public class Menu
    {
        private readonly ConsoleUI _consoleUI;
        private readonly ManageDrinksService _manageDrinksService;
        private readonly FavouriteDrinkService _favouriteDrinkService;
        private enum CategoryOptions
        {
            DrinksMenu,
            MyFavourites,
            Exit
        }

        private enum DrinksOptions
        {
            ViewAll,
            SearchDrinks,
            Goback
        }

        private enum MyFavouritesOptions
        {
            ViewAll,
            ViewCount,
            GoBack
        }

        private T PromptEnum<T>(string title, params (string Label, T Value)[] options) where T : struct
        {
            var labels = new List<string>();
            var map = new Dictionary<string, T>();
            foreach (var (label, value) in options)
            {
                labels.Add(label);
                map[label] = value;
            }

            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(title)
                    .AddChoices(labels));

            return map[selected];
        }

        public Menu()
        {
            _consoleUI = new ConsoleUI();
            _manageDrinksService = new ManageDrinksService();
            _favouriteDrinkService = new FavouriteDrinkService();
        }

        public async Task Start()
        {
            var running = true;
            while (running)
            {
                _consoleUI.ShowHeader();
                var choice = PromptEnum<CategoryOptions>(
                    "Choose your operation",
                    ("Drinks Menu", CategoryOptions.DrinksMenu),
                    ("My Favourites", CategoryOptions.MyFavourites),
                    ("Exit", CategoryOptions.Exit));

                switch (choice)
                {
                    case CategoryOptions.DrinksMenu:
                       await ManageDrinksAsync();
                        break;
                    case CategoryOptions.MyFavourites:
                        ManageMyFavourites();
                        break;
                    case CategoryOptions.Exit:
                        running = false;
                        _consoleUI.ShowGoodbye();
                        break;
                }
            }
        }

        private async Task  ManageDrinksAsync()
        {
            var isSubMenu = true;
            while (isSubMenu)
            {
                _consoleUI.ShowHeader();
                var choice = PromptEnum<DrinksOptions>(
                    "Choose your operation",
                    ("View All Categories", DrinksOptions.ViewAll),
                    ("Search Drink", DrinksOptions.SearchDrinks),
                    ("Go Back", DrinksOptions.Goback));

                try
                {
                    switch (choice)
                    {
                        case DrinksOptions.ViewAll:
                            await _manageDrinksService.ViewAllCategoriesAsync();
                            break;
                        case DrinksOptions.SearchDrinks:
                            await _manageDrinksService.SearchDrinksAsync();
                            break;
                        case DrinksOptions.Goback:
                            isSubMenu = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _consoleUI.ShowError(ex.Message);
                    _consoleUI.Pause();
                }
            }
        }

        private void ManageMyFavourites()
        {
            var isSubMenu = true;
            while (isSubMenu)
            {
                _consoleUI.ShowHeader();
                var choice = PromptEnum<MyFavouritesOptions>(
                    "Choose your operation",
                    ("View All Favourite Drinks", MyFavouritesOptions.ViewAll),
                    ("Most Viewed Drink", MyFavouritesOptions.ViewCount),
                    ("Go Back", MyFavouritesOptions.GoBack));

                try
                {
                    switch (choice)
                    {
                        case MyFavouritesOptions.ViewAll:
                            _favouriteDrinkService.ViewAllDrinks();
                            _consoleUI.Pause();
                            break;
                        case MyFavouritesOptions.ViewCount:
                            _favouriteDrinkService.DisplayMostViewedDrink();
                            _consoleUI.Pause();
                            break;
                        case MyFavouritesOptions.GoBack:
                            isSubMenu = false;
                            break;
                    }
                    
                    AnsiConsole.Clear();
                }
                catch (Exception ex)
                {
                    _consoleUI.ShowError(ex.Message);
                    _consoleUI.Pause();
                }
            }
        }
    }
}
