using DrinksInfo.Application.DrinkInfoApi.GetCategoryList;
using DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class CategoryListService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly DrinkDetailService _drinkDetailService;
    private readonly GetCategoryListHandler _getCategoryListHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;
    private readonly Messages _messages;

    public CategoryListService(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
                                GetCategoryListHandler getCategoryListHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler,
                                DrinkDetailService drinkDetailService, Messages messages)
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _getCategoryListHandler = getCategoryListHandler;
        _getDrinksHandler = getDrinksHandler;
        _drinkDetailService = drinkDetailService;
        _messages = messages;
    }

    public async Task RunAsync()
    {
        Console.Clear();
        var categoryListResult = await ConsoleStatusHelper.ShowStatusAsync("Fetching category list...", () =>
                                            _getCategoryListHandler.HandleAsync());

        if (categoryListResult.IsSuccess && categoryListResult.Value is not null)
            await ManageCategorySelection(categoryListResult.Value);
        else
            _messages.OutputErrorMessage(categoryListResult.Errors);
    }

    private async Task ManageCategorySelection(List<CategoryResponse> category)
    {
        var exitCode = ExitCode.None;
        bool returnToMainMenu = false;

        while (returnToMainMenu == false)
        {
            Console.Clear();
            var categorySelection = _categorySelection.Render(category.ToArray());
            int selectionIndex = category.FindIndex(category => category.Name == categorySelection);


            var drinksResult = await ConsoleStatusHelper.ShowStatusAsync($"Fetching drinks in {categorySelection}...", () =>
                                            _getDrinksHandler.HandleAsync(category[selectionIndex].Name));

            if (drinksResult.IsSuccess && drinksResult.Value is not null)
                exitCode = await ManageDrinkDetails(category[selectionIndex].Name, drinksResult.Value);
            else
                _messages.OutputErrorMessage(drinksResult.Errors);

            if (exitCode == ExitCode.MainMenu)
                returnToMainMenu = true;
            else
                returnToMainMenu = false;
        }
    }

    private async Task<ExitCode> ManageDrinkDetails(string categoryName, List<DrinkSummaryResponse> drinks)
    {
        var exitCode = ExitCode.None;
        bool returnToCategoryMenu = false;

        while (returnToCategoryMenu == false)
        {
            var drinkSelection = _drinkListSelection.Render(categoryName, drinks);

            exitCode = await _drinkDetailService.RunAsync(DrinkDetailEntryMode.Category, drinkSelection);

            if (exitCode == ExitCode.CategorySelection || exitCode == ExitCode.MainMenu)
                returnToCategoryMenu = true;
            else
                returnToCategoryMenu = false;
        }
        return exitCode;
    }
}
