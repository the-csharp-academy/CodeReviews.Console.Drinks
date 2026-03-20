using DrinksInfo.Application.DrinkInfoApi.GetCategoryList;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class CategoryListSelectionView
{
    public string Render(CategoryResponse[] categories)
    {
        var selection = AnsiConsole.Prompt(
                            new SelectionPrompt<CategoryResponse>()
                            .Title("Select a category from below: ")
                            .PageSize(15)
                            .WrapAround()
                            .UseConverter(category => $"{category.Name}")
                            .AddChoices(categories));

        return selection.Name;
    }
}
