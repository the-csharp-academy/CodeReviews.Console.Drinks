using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainMenuService>();
        services.AddTransient<CategoryListService>();
        services.AddTransient<DrinkDetailService>();
        services.AddTransient<FavoriteDrinkServices>();
        services.AddTransient<GetViewCountService>();

        services.AddTransient<CategoryListSelectionView>();
        services.AddTransient<DrinkListSelectionView>();
        services.AddTransient<DrinkDetailsView>();
        services.AddTransient<DrinkImageView>();
        services.AddTransient<MainMenuView>();
        services.AddTransient<FavoriteDrinkListView>();

        services.AddTransient<Messages>();

        return services;
    }
}
