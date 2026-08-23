using Spectre.Console;

namespace Drinks;

internal class ViewsList : DrinkIdName
{
    internal int Views;

    public ViewsList(int id, string name, int views)
    : base(id, name)
    {
        Views = views;
    }
    internal static List <ViewsList> viewsList = new();

    internal static void AddToCount(int id, string name)
    {
        if(viewsList.Count == 0)
        {
            ViewsList newView = new ViewsList(id, name, 1);
            viewsList.Add(newView);
        }
        else
        {
            int counter = 0;

            foreach(ViewsList view in viewsList)
            {
                if(view.Name == name && view.Id == id)
                {
                    viewsList[counter].Views++;
                    return;
                }
                counter++;
            }

            ViewsList newView = new ViewsList(id, name, 1);
            viewsList.Add(newView);
        }
    }

    internal static void PrintList()
    {
        if(viewsList.Count == 0)
        {
            Console.WriteLine("You didn't see any drink yet.\n");
            return;
        }

        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Views[/]");

        foreach(var view in viewsList)
        {
            table.AddRow(view.Id.ToString(), view.Name, view.Views.ToString());
        }

        AnsiConsole.Write(table);
    }
}