using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;

using static Drinks.Enums;



namespace Drinks;

static class DrinkService
{
    static HttpClient client = new HttpClient();
    public static void GetCategories()
    {
        try
        {
            var response = serializer("", Enums.Type.Category);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content.ReadAsStringAsync().Result;

                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse)!;

                List<Category> returnedList = serialize.CategoriesList!;

                if (returnedList != null && returnedList!.Count > 0)
                {
                    TableVisualizationEngine.ShowTable(returnedList, "Categories:");
                }
                else
                {
                    Console.WriteLine("Error.");
                }

            }
            else
            {
                Console.Write("There was an error retrieving the response.");
            }
        }
        catch (Exception e)
        {
            Console.Write("GetCategories: ");
            Console.WriteLine(e.Message);
        }
    }

    public static void GetDrinksByCategory(string category, MenuActions choice)
    {
        try
        {

            var response = serializer(category, Enums.Type.Drink);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content.ReadAsStringAsync().Result;

                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse)!;

                List<Drink> returnedList = serialize.drinksList!;

                if (returnedList != null && returnedList!.Count > 0)
                {
                    TableVisualizationEngine.ShowTable(returnedList, "Drinks:");
                }
                else
                {
                    Console.WriteLine("Error.");
                }
            }
            else
            {
                Console.Write("There was an error retrieving the response.");
            }
        }
        catch (Exception e)
        {
            Console.Write("GetDrinksByCategory: ");
            Console.WriteLine(e.Message);
        }
    }

    public static void GetDrinkDetails(string drink, MenuActions choice)
    {
        try
        {

            var response = serializer(drink, Enums.Type.DrinkDetail); 
            
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content.ReadAsStringAsync().Result;

                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse)!;

                List<DrinkDetail>? returnedList = serialize.DrinkDetailList;

                if (returnedList != null && returnedList!.Count > 0)
                {
                    TableVisualizationEngine.ShowTable(returnedList, "Ingredients:");

                    switch (choice)
                    {
                        case MenuActions.SaveFavorite:
                            FavoritesList.CheckDouble(Convert.ToInt32(drink), returnedList[0].strDrink!);
                            break;
                        case MenuActions.OrderDrink:
                            Console.WriteLine("Drink is being prepared...");

                            Process.Start(
                                new ProcessStartInfo
                                {
                                    FileName = returnedList[0].strDrinkThumb,
                                    UseShellExecute = true
                                }
                            );
                            break;
                    }
                    ;

                    ViewsList.AddToCount(Convert.ToInt32(drink), returnedList[0].strDrink!);

                }
                else
                {
                    Console.WriteLine("Error.");
                }
            }
            else
            {
                Console.Write("There was an error retrieving the response.");
            }
        }
        catch (Exception e)
        {
            Console.Write("GetDrinkDetails: ");
            Console.WriteLine(e.Message);
        }
    }

    public static Task<HttpResponseMessage> serializer(string url, Enums.Type type)
    {
        Task<HttpResponseMessage> response;

        switch (type)
        {
            case Enums.Type.Category:
                response = client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
                break;
            case Enums.Type.Drink:
                response = client.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={HttpUtility.UrlEncode(url)}");
                break;
            case Enums.Type.DrinkDetail:
                response = client.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={url}");
                break;
            default:
                response = client.GetAsync("");
                break;
        }
        ;
        return response;
    }
}