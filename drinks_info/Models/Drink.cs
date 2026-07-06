using Newtonsoft.Json;
using System;

namespace drinks_info.Models
{
    public class Drink
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
    }


public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinksList { get; set; }
    }
}