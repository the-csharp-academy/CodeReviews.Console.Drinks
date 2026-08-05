using drinks_info.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using WebAPIClient.Models;
using WebAPIClient.UserInterface;

namespace WebAPIClient.Services
{
    internal class DrinksService
    {
        private readonly HttpClient _httpClient;

        public DrinksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://www.thecocktaildb.com/api/json/v1/1/");
        }

        public async Task<List<Category>> GetCategories()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "list.php?c=list");
            using var responce = await _httpClient.SendAsync(request);

            var returnedList = new List<Category>();
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = await responce.Content.ReadAsStringAsync();
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                returnedList = serialize.CategoriesList;

                await TableVisualisation.ShowTable(returnedList, "Categories Menu");

                return returnedList;
            }
            else throw new Exception("Destination address is unavailable");
        }

        internal async Task GetDrink(string? drink)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"lookup.php?i={drink}");
            using var responce = await _httpClient.SendAsync(request);

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = await responce.Content.ReadAsStringAsync();
                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);

                List<DrinkDetail> returnedList = serialize.DrinkDetailList;

                DrinkDetail drinkDetail = returnedList[0];

                List<DrinkProperty> prepList = new();
                string formattedName = "";

                foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
                {
                    if (prop.Name.Contains("str"))
                    {
                        formattedName = prop.Name.Substring(3);
                    }

                    if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                    {
                        prepList.Add(new DrinkProperty
                        {
                            Section = formattedName,
                            Description = prop.GetValue(drinkDetail)
                        });
                    }
                }
                await TableVisualisation.ShowTable(prepList, drinkDetail.strDrink);
            }
            else throw new Exception("Destination address is unavailable");
        }

        internal async Task<List<Drink>> GetDrinksByCategory(string category)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"filter.php?c={HttpUtility.UrlEncode(category)}");
            using var responce = await _httpClient.SendAsync(request);

            var returnedList = new List<Drink>();
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = await responce.Content.ReadAsStringAsync();
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                returnedList = serialize.DrinksList;

                await TableVisualisation.ShowTable(returnedList, "Drinks List");

                return returnedList;
            }
            else throw new Exception("Destination address is unavailable");
        }
    }
}
