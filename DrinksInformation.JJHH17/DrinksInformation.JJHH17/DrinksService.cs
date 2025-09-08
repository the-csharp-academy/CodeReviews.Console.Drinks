using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DrinksInformation.JJHH17
{
    internal class DrinksService
    {
        public void GetCategories()
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1"); // TODO: PASS THESE AS APP.CONFIG VALUES
        }
    }
}
