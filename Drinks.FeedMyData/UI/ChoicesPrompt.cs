using Spectre.Console;

namespace Drinks;

internal class ChoicesPrompt
{
    internal static Category SelectOne(List<Category> choices, string title)
    {
        Category empty = new("");

        var selection = AnsiConsole.Prompt(
        new SelectionPrompt<Category>()
        .Title(title)
        .UseConverter(entry => entry.strCategory)
        .PageSize(20)
        .AddChoices(choices)
        .AddCancelResult(empty));

        return selection;
    }

    internal static Drink SelectOne(List<Drink> choices, string title)
    {
        Drink empty = new("", "");

        var selection = AnsiConsole.Prompt(
        new SelectionPrompt<Drink>()
        .Title(title)
        .UseConverter(entry => entry.strDrink)
        .PageSize(20)
        .AddChoices(choices)
        .AddCancelResult(empty));

        return selection;
    }
}