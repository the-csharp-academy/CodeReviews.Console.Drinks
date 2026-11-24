using Spectre.Console;

namespace Drinks;

internal class ChoicesPrompt
{
    internal static Category SelectOne(List<Category> choices, string title, string color)
    {
        var selection = AnsiConsole.Prompt(
        new SelectionPrompt<Category>()
        .Title($"Select the {title}")
        .HighlightStyle(color)
        .UseConverter(entry => entry.strCategory)
        .PageSize(100)
        .AddChoices(choices));

        return selection;
    }
}