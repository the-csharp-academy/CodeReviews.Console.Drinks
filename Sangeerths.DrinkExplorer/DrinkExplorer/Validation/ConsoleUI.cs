using Spectre.Console;

namespace DrinkExplorer.Validation
{
    public class ConsoleUI
    {
         public void ShowHeader()
        {
            AnsiConsole.Clear();

            var header = new FigletText("DrinkExplorer").Centered().Color(Color.Aqua);
            AnsiConsole.Write(header);
            AnsiConsole.WriteLine();
            var rule = new Rule
            {
                Justification = Justify.Center,
                Style = new Style(Color.Aqua)
            };
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold cyan]Welcome to DrinkExplorer[/]");
            AnsiConsole.MarkupLine("[grey]Discover drinks, explore recipes, and manage your favourites.[/]");
            AnsiConsole.WriteLine();
        }

        public void Pause()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }

        public void ShowSuccess(string message)
        {
            AnsiConsole.MarkupLine($"[bold green][[ SUCCESS ]][/] {message}");
        }

        public void ShowError(string message)
        {
            AnsiConsole.MarkupLine($"[bold red][[ ERROR ]][/] {message}");
        }

        public void ShowInfo(string message)
        {
            AnsiConsole.MarkupLine($"[bold cyan][[ INFO ]][/] {message}");
        }

        public void ShowGoodbye()
        {
            AnsiConsole.Clear();
            var goodbyeText = new FigletText("Goodbye!").Centered().Color(Color.Aqua);
            AnsiConsole.Write(goodbyeText);
            AnsiConsole.WriteLine();

            var panel = new Panel(new Markup("[bold cyan]Thank you for using DrinkExplorer![/]\n[grey]We hope to see you again soon to discover more recipes.[/]"))
            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Aqua),
                Padding = new Padding(3, 1, 3, 1),
                Expand = false
            };

            AnsiConsole.Write(Align.Center(panel));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
        }
    }
}