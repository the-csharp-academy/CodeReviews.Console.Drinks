using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Helpers;

public static class ConsoleStatusHelper
{
    public static async Task<T> ShowStatusAsync<T>(string statusMessage, Func<Task<T>> action)
    {
        return await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .SpinnerStyle(Style.Parse("bold cyan"))
            .StartAsync(statusMessage, async ctx =>
            {
                return await action();
            });
    }
}
