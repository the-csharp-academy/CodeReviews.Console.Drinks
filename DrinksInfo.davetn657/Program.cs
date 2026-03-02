using DrinksInfo.davetn657.Views;

namespace DrinksInfo.davetn657;

class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(200, 40);

        var userInterface = new UserInterface();

        userInterface.StartApplication();
    }
}