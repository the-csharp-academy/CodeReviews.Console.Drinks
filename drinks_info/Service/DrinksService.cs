using System;
using System.Reflection;
using System.Web;
using drinks_info.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace drinks_info.Service
{
    public class DrinksService
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://www.thecocktaildb.com/api/json/v1/1/") };

        public List<Category> GetCategories()
        {
            var response = _httpClient.GetAsync("list.php?c=list").GetAwaiter().GetResult();

            List<Category> categories = new();

            if (response.IsSuccessStatusCode)
            {
                string rawResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;

                TableVisualisationEngine.ShowTable(categories, "Categories Menu");
                return categories;
            }
            return categories;
        }

        public List<Drink> GetDrinksByCategory(string category)
        {
            var response = _httpClient.GetAsync($"filter.php?c={HttpUtility.UrlEncode(category)}").GetAwaiter().GetResult();

            List<Drink> drinks = new();

            if (response.IsSuccessStatusCode)
            {
                string rawResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                drinks = serialize.DrinksList;

                TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");
                return drinks;
            }
            return drinks;
        }

        public DrinkDetail GetDrink(string drink)
        {
            var response = _httpClient.GetAsync($"lookup.php?i={HttpUtility.UrlEncode(drink)}").GetAwaiter().GetResult();

            DrinkDetail drinkDetail = new();

            if (response.IsSuccessStatusCode)
            {
                string rawResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);

                List<DrinkDetail> returnedList = serialize.DrinkDetailList;

                drinkDetail = returnedList[0];

                List<object> prepList = new();

                string formattedName = "";

                foreach(PropertyInfo prop in drinkDetail.GetType().GetProperties())
                {
                    formattedName = prop.Name;
                    if (prop.Name.Contains("str"))
                    {
                        formattedName = prop.Name.Substring(3);
                    }

                    if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                    {
                        prepList.Add(new
                        {
                            Key = formattedName,
                            Value = prop.GetValue(drinkDetail)
                        });
                    }
                }
                TableVisualisationEngine.ShowTable(prepList, drinkDetail.strDrink);
                
                if (!string.IsNullOrEmpty(drinkDetail.strDrinkThumb))
                {
                    TableVisualisationEngine.ShowImage(drinkDetail.strDrinkThumb);
                }

                Console.WriteLine("\n\n");

                return drinkDetail;
            }
            return drinkDetail;
        }

        public Drink? GetDrinkById(string drinkId)
        {
            var response = _httpClient.GetAsync($"lookup.php?i={HttpUtility.UrlEncode(drinkId)}").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                string rawResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(rawResponse))
                {
                    var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);
                    if (serialize?.DrinkDetailList != null && serialize.DrinkDetailList.Count > 0)
                    {
                        var detail = serialize.DrinkDetailList[0];
                        return new Drink
                        {
                            idDrink = detail.idDrink,
                            strDrink = detail.strDrink
                        };
                    }
                }
            }
            return null;
        }
    }
}
