using DrinksInfo.Application;
using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.Infrastructure;
using DrinksInfo.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace DrinksInfo.ConsoleUI;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        services.AddInfrastructure(config);
        services.AddPresentation();
        services.AddApplication();

        var provider = services.BuildServiceProvider();

        provider.GetRequiredService<IDatabaseInitializer>().Run();

        var mainMenu = provider.GetRequiredService<MainMenuService>();

        await mainMenu.RunAsync();
    }
}