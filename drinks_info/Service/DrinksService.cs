using System;
using System.Reflection;
using System.Web;
using drinks_info.Models;
using Newtonsoft.Json;
using RestSharp;

namespace drinks_info.Service
{
    public class DrinksService
    {
        public List<Category> GetCategories()
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1");
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);

            List<Category> categories = new();

            if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;

                TableVisualisationEngine.ShowTable(categories, "Categories Menu");
                return categories;
            }
            return categories;
        }

        public List<Drink> GetDrinksByCategory(string category)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1");
            var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
            var response = client.ExecuteAsync(request);

            List<Drink> drinks = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                drinks = serialize.DrinksList;

                TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");
                return drinks;
            }
            return drinks;
        }

        public void GetDrink(string drink)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1");
            var request = new RestRequest($"lookup.php?i={HttpUtility.UrlEncode(drink)}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);

                List<DrinkDetail> returnedList = serialize.DrinkDetailList;

                DrinkDetail drinkDetail = returnedList[0];

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
            }
        }
    }
}
