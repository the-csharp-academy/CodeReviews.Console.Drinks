using DrinksInfo.Application.Interfaces;
using DrinksInfo.Infrastructure.Interfaces;
using DrinksInfo.Infrastructure.Repositories;
using DrinksInfo.Infrastructure.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace DrinksInfo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IDrinkRepository, DrinkRepository>(client =>
        {
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddTransient<ISqliteConnectionFactory, SqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

        services.AddTransient<IFavoriteDrinkRepository, FavoriteDrinkRepository>();
        services.AddTransient<IDrinkViewCountRepository, DrinkViewCountRepository>();

        return services;
    }
}
