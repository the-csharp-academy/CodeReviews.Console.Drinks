using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ConsoleTableExt;
using DrinksInformation.JJHH17.Models;


namespace DrinksInformation.JJHH17
{
    internal class DrinksService
    {
        public void GetCategories()
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1"); 
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                List<Category> returnedList = serialize.CategoriesList;

                TableVisualisationEngine.ShowTable(returnedList, "Categories Menu");
            }
        }

        internal void GetDrinksByCategory(string category)
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1"); 
            var request = new RestRequest($"filter.php?c={category}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
                List<Drink> returnedList = serialize.Drinks;
                TableVisualisationEngine.ShowTable(returnedList, "Drinks Menu");
            }

        }
    }
}
