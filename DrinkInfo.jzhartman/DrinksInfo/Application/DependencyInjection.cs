using DrinksInfo.Application.DrinkInfoApi.GetCategoryList;
using DrinksInfo.Application.DrinkInfoApi.GetDrinkDetailsById;
using DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;
using DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;
using DrinksInfo.Application.Favorites.AddFavoriteDrink;
using DrinksInfo.Application.Favorites.DeleteFavoriteDrink;
using DrinksInfo.Application.Favorites.ExistsById;
using DrinksInfo.Application.Favorites.GetAllFavoriteDrinks;
using DrinksInfo.Application.ViewCount.AddByDrinkId;
using DrinksInfo.Application.ViewCount.ExistsById;
using DrinksInfo.Application.ViewCount.GetById;
using DrinksInfo.Application.ViewCount.UpdateById;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetCategoryListHandler>();
        services.AddTransient<GetDrinksSummaryByCategoryNameHandler>();
        services.AddTransient<GetDrinkDetailsByIdHandler>();
        services.AddTransient<GetDrinkImageHandler>();

        services.AddTransient<GetAllFavoriteDrinksHandler>();
        services.AddTransient<AddFavoriteDrinkHandler>();
        services.AddTransient<DeleteFavoriteDrinkByIdHandler>();
        services.AddTransient<FavoriteExistsByIdHandler>();

        services.AddTransient<AddViewCountByDrinkIdHandler>();
        services.AddTransient<ViewCountExistsByIdHandler>();
        services.AddTransient<UpdateViewCountByIdHandler>();
        services.AddTransient<GetViewCountByIdHandler>();

        return services;
    }
}
